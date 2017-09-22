using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.application.ETL.DataSource;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.ETL.Processor
{
    public class ArticleCategoryProcessor : ICategoryProcessor
    {
        private readonly ICategoryDataSource _categoryDataSource;
        private readonly IArticleBatchProcessor _articleBatchProcessor;

        public ArticleCategoryProcessor(ICategoryDataSource categoryDataSource, IArticleBatchProcessor articleBatchProcessor)
        {
            _categoryDataSource = categoryDataSource;
            _articleBatchProcessor = articleBatchProcessor;
        }

        public Task<ArticleBatchTaskResult> Process(string category, int pageSize)
        {
            var response = new ArticleBatchTaskResult { Category = category };

            var processorCount = Environment.ProcessorCount;

            // Pipeline members
            var articleBatchBufferBlock = new BufferBlock<UnexpandedArticle[]>();
            var articleTransformBlock = new TransformBlock<UnexpandedArticle[], ArticleBatchTaskResult>(articles => _articleBatchProcessor.Process(category, articles));
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

            // Process "Category" and generate article batch data
            _categoryDataSource.Producer(category, pageSize, articleBatchBufferBlock);

            // Mark the head of the pipeline as complete. The continuation tasks  
            // propagate completion through the pipeline as each part of the  
            // pipeline finishes.
            articleActionBlock.Completion.Wait();

            return Task.FromResult(response);
        }
    }
}