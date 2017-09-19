using System;
using System.Configuration;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.Model;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.application.ETL.BatchItemProcessor.CardProcessor
{
    public class CardItemProcessor : IBatchItemProcessor
    {
        private readonly ICardWebPage _cardWebPage;

        public CardItemProcessor(ICardWebPage cardWebPage)
        {
            _cardWebPage = cardWebPage;
        }

        public Task<Card> ProcessItem(UnexpandedArticle item)
        {
            var domainUrl = ConfigurationManager.AppSettings["domainUrl"];

            var yugiohCard = _cardWebPage.GetYugiohCard(new Uri(new Uri(domainUrl), item.Url));
        }

        public bool Handles(string category)
        {
            return category == ArticleCategory.TcgCards || category == ArticleCategory.OcgCards;
        }
    }
}