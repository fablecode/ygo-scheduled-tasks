using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using wikia.Api;
using wikia.Models.Article;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.ETL.Processor
{
    public class CategoryProcessor : ICategoryProcessor
    {
        private readonly IWikiArticle _wikiArticle;
        private readonly IArticleBatchProcessor _articleBatchProcessor;

        public CategoryProcessor(IWikiArticle wikiArticle, IArticleBatchProcessor articleBatchProcessor)
        {
            _wikiArticle = wikiArticle;
            _articleBatchProcessor = articleBatchProcessor;
        }

        public Task<ArticleBatchTaskResult> Process(string category, int pageSize)
        {
            var response = new ArticleBatchTaskResult { Category = category };
            var processorCount = Environment.ProcessorCount;

            // Pipeline members
            var articleBatchBufferBlock = new BufferBlock<UnexpandedArticle[]>();
            var articleTransformBlock = new TransformBlock<UnexpandedArticle[], ArticleBatchTaskResult>(t => _articleBatchProcessor.Process(category, t));
            var articleActionBlock = new ActionBlock<ArticleBatchTaskResult>(delegate (ArticleBatchTaskResult result)
                {
                    response.Processed += result.Processed;
                    response.Failed = result.Failed;
                },
                // Specify a maximum degree of parallelism.
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = processorCount
                });

            // Form the pipeline
            articleBatchBufferBlock.LinkTo(articleTransformBlock);
            articleTransformBlock.LinkTo(articleActionBlock);

            //  Create the completion tasks:
            articleBatchBufferBlock.Completion
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        ((IDataflowBlock)articleTransformBlock).Fault(t.Exception);
                    else
                        articleTransformBlock.Complete();
                });

            articleTransformBlock.Completion
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        ((IDataflowBlock)articleActionBlock).Fault(t.Exception);
                    else
                        articleActionBlock.Complete();
                });

            // Process the "Category" and generate article batch
            var producer = Producer(category, pageSize, articleBatchBufferBlock);

            // Mark the head of the pipeline as complete. The continuation tasks  
            // propagate completion through the pipeline as each part of the  
            // pipeline finishes.
            articleActionBlock.Completion.Wait();

            return Task.FromResult(response);
        }

        #region private helpers

        private async Task Producer(string category, int pageSize, BufferBlock<UnexpandedArticle[]> articleBufferBlock)
        {
            var nextBatch = await _wikiArticle.AlphabeticalList(new ArticleListRequestParameters { Category = category, Limit = pageSize });

            bool isNextBatchAvailable;

            do
            {
                articleBufferBlock.Post(nextBatch.Items);

                isNextBatchAvailable = !string.IsNullOrEmpty(nextBatch.Offset);

                if (isNextBatchAvailable)
                {
                    nextBatch = await _wikiArticle.AlphabeticalList(new ArticleListRequestParameters
                    {
                        Category = category,
                        Limit = pageSize,
                        Offset = nextBatch.Offset
                    });
                }
            } while (isNextBatchAvailable);
        }

        #endregion
    }
}