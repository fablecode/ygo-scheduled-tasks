using System.Threading.Tasks;
using MediatR;
using wikia.Api;
using wikia.Models.Article;
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

        public async Task<CategoryTaskResult> Handle(AllCardsTask message)
        {
            var response = new CategoryTaskResult { Category = message.Category };

            var nextBatch = await _wikiArticle.AlphabeticalList(new ArticleListRequestParameters { Category = message.Category, Limit = message.PageSize });

            bool isNextBatchAvailable;

            do
            {
                var result = await _mediator.Send(new CardBatchTask { Category = message.Category, Items = nextBatch.Items});

                response.Processed += result.Processed;
                response.Failed = result.Failed;

                isNextBatchAvailable = !string.IsNullOrEmpty(nextBatch.Offset);

                if (isNextBatchAvailable)
                {
                    nextBatch = await _wikiArticle.AlphabeticalList(new ArticleListRequestParameters { Category = message.Category, Limit = message.PageSize, Offset = nextBatch.Offset });
                }

            } while (isNextBatchAvailable);


            return response;

        }
    }
}