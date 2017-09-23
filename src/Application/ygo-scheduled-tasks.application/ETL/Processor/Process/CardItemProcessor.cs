using System;
using System.Configuration;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.application.Dto;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;
using ygo_scheduled_tasks.domain.Model;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.application.ETL.Processor.Process
{
    public class CardItemProcessor : IBatchItemProcessor
    {
        private readonly IConfig _config;
        private readonly ICardWebPage _cardWebPage;
        private readonly IYugiohCardService _yugiohCardService;

        public CardItemProcessor(IConfig config, ICardWebPage cardWebPage, IYugiohCardService yugiohCardService)
        {
            _config = config;
            _cardWebPage = cardWebPage;
            _yugiohCardService = yugiohCardService;
        }

        public Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item)
        {
            var yugiohCard = _cardWebPage.GetYugiohCard(new Uri(new Uri(_config.DomainUrl), item.Url));

            var card = _yugiohCardService.AddOrUpdate(yugiohCard);

            return Task.FromResult(new ArticleTaskResult());
        }

        public bool Handles(string category)
        {
            return category == ArticleCategory.TcgCards || category == ArticleCategory.OcgCards;
        }
    }

    public interface IYugiohCardService
    {
        CardDto AddOrUpdate(YugiohCard yugiohCard);
    }
}