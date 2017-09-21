using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using MediatR;
using wikia.Api;
using wikia.Models.Article;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.application.ETL.CardBatch;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.ETL.AllCards
{
    public class AllCardsTaskHandler : IAsyncRequestHandler<AllCardsTask, CategoryTaskResult>
    {
        private readonly IMediator _mediator;
        private readonly IWikiArticle _wikiArticle;

        public AllCardsTaskHandler(IMediator mediator, IWikiArticle wikiArticle)
        {
            _mediator = mediator;
            _wikiArticle = wikiArticle;
        }

        public Task<CategoryTaskResult> Handle(AllCardsTask message)
        {
            var response = new CategoryTaskResult {Category = message.Category};

            int processorCount = Environment.ProcessorCount;
            int messageCount = processorCount;

            // dataflow tpl blocks
            var articleBatchBufferBlock = new BufferBlock<UnexpandedArticle[]>();
            var articleTransformBlock = new TransformBlock<UnexpandedArticle[], CategoryTaskResult>(t => _mediator.Send(new CardBatchTask { Category = message.Category, Items = t }));
            var articleActionBlock = new ActionBlock<CategoryTaskResult>(delegate(CategoryTaskResult result)
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

            // Process the "Category"
            var producer = Producer(message, articleBatchBufferBlock);

            // Mark the head of the pipeline as complete. The continuation tasks  
            // propagate completion through the pipeline as each part of the  
            // pipeline finishes.
            articleActionBlock.Completion.Wait();

            return Task.FromResult(response);

        }

        private async Task Producer(AllCardsTask message, BufferBlock<UnexpandedArticle[]> articleBufferBlock)
        {
            var nextBatch =
                await _wikiArticle.AlphabeticalList(
                    new ArticleListRequestParameters {Category = message.Category, Limit = message.PageSize});

            bool isNextBatchAvailable;

            do
            {
                //var result = await _mediator.Send(new CardBatchTask { Category = message.Category, Items = nextBatch.Items});

                //response.Processed += result.Processed;
                //response.Failed = result.Failed;

                articleBufferBlock.Post(nextBatch.Items);

                isNextBatchAvailable = !string.IsNullOrEmpty(nextBatch.Offset);

                if (isNextBatchAvailable)
                {
                    nextBatch = await _wikiArticle.AlphabeticalList(new ArticleListRequestParameters
                    {
                        Category = message.Category,
                        Limit = message.PageSize,
                        Offset = nextBatch.Offset
                    });
                }
            } while (isNextBatchAvailable);
        }
    }
}