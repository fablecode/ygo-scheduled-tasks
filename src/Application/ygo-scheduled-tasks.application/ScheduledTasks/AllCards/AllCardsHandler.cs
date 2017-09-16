using System;
using System.Threading.Tasks;
using MediatR;
using wikia.Api;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.ScheduledTasks.AllCards
{
    public class AllCardsHandler : IAsyncRequestHandler<AllCardsTask, CategoryTaskResult>
    {
        private readonly IWikiArticle _wikiArticle;

        public AllCardsHandler(IWikiArticle wikiArticle)
        {
            _wikiArticle = wikiArticle;
        }

        public Task<CategoryTaskResult> Handle(AllCardsTask message)
        {
            //var response = new CategoryTaskResult { Category = message.Category };

            //var processedCards = new List<string>();

            //var nextBatch = await _wikiArticle.AlphabeticalList(new ArticleListRequestParameters { Category = message.Category, Limit = message.PageSize});

            //bool isNextBatchAvailable;

            //do
            //{
            //    var result = await _articleBatchProcessor.ProcessBatch(message.Category, nextBatch);

            //    processedCards.AddRange(result);

            //    isNextBatchAvailable = !string.IsNullOrEmpty(nextBatch.Offset);

            //    if (isNextBatchAvailable)
            //    {

            //        nextBatch = await _wikiArticle.AlphabeticalList(new ArticleListRequestParameters { Category = message.Category, Limit = message.PageSize, Offset = nextBatch.Offset});
            //    }

            //} while (isNextBatchAvailable);


            //return response;

            throw new NotImplementedException();
        }
    }
}