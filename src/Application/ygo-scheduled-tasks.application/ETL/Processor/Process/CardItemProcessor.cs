using System;
using System.Configuration;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.application.ETL.Processor.Process
{
    public class CardItemProcessor : IBatchItemProcessor
    {
        private readonly ICardWebPage _cardWebPage;

        public CardItemProcessor(ICardWebPage cardWebPage)
        {
            _cardWebPage = cardWebPage;
        }

        public Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item)
        {
            var domainUrl = ConfigurationManager.AppSettings["domainUrl"];

            var yugiohCard = _cardWebPage.GetYugiohCard(new Uri(new Uri(domainUrl), item.Url));

            return Task.FromResult(new ArticleTaskResult());
        }

        public bool Handles(string category)
        {
            return category == ArticleCategory.TcgCards || category == ArticleCategory.OcgCards;
        }
    }
}