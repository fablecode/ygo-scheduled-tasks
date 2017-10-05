﻿using System;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.domain.ETL.Processor.Process
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

        public async Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item)
        {
            var response = new ArticleTaskResult { Article = item };

            var yugiohCard = _cardWebPage.GetYugiohCard(new Uri(new Uri(_config.WikiaDomainUrl), item.Url));

            var card = await _yugiohCardService.AddOrUpdate(yugiohCard);

            if (card != null)
                response.Processed = true;

            return response;
        }

        public bool Handles(string category)
        {
            return category == ArticleCategory.TcgCards || category == ArticleCategory.OcgCards;
        }
    }
}