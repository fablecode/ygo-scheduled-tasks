using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL.Article.DataSource;
using ygo_scheduled_tasks.domain.ETL.Article.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.Article.Processor
{
    public class ArticleCategoryProcessor : IArticleCategoryProcessor
    {
        private readonly IArticleBatchProcessor _articleBatchProcessor;
        private readonly IArticleCategoryDataSource _articleCategoryDataSource;

        public ArticleCategoryProcessor(IArticleCategoryDataSource articleCategoryDataSource, IArticleBatchProcessor articleBatchProcessor)
        {
            _articleCategoryDataSource = articleCategoryDataSource;
            _articleBatchProcessor = articleBatchProcessor;
        }

        public Task<ArticleBatchTaskResult> Process(string category, int pageSize)
        {
            var response = new ArticleBatchTaskResult {Category = category};

            var processorCount = 2;

            // Pipeline members
            var articleBatchBufferBlock = new BufferBlock<UnexpandedArticle[]>();
            var articleTransformBlock = new TransformBlock<UnexpandedArticle[], ArticleBatchTaskResult>(articles => _articleBatchProcessor.Process(category, articles));
            var articleActionBlock = new ActionBlock<ArticleBatchTaskResult>(delegate(ArticleBatchTaskResult result)
            {
                response.Processed += result.Processed;
                response.Failed.AddRange(result.Failed);
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
                        ((IDataflowBlock) articleTransformBlock).Fault(t.Exception);
                    else
                        articleTransformBlock.Complete();
                });

            articleTransformBlock.Completion
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        ((IDataflowBlock) articleActionBlock).Fault(t.Exception);
                    else
                        articleActionBlock.Complete();
                });

            // Process "Category" and generate article batch data
            _articleCategoryDataSource.Producer(category, pageSize, articleBatchBufferBlock);

            // Mark the head of the pipeline as complete. The continuation tasks  
            // propagate completion through the pipeline as each part of the  
            // pipeline finishes.
            articleActionBlock.Completion.Wait();

            return Task.FromResult(response);
        }
    }
}