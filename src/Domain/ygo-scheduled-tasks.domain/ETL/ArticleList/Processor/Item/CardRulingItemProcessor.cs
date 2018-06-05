﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wikia.Api;
using wikia.Models.Article.AlphabeticalList;
using wikia.Models.Article.Simple;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item
{
    public class CardRulingItemProcessor : IArticleItemProcessor
    {
        private readonly IWikiArticle _wikiArticle;
        private readonly ICardService _cardService;
        private readonly ICardRulingService _cardRulingService;

        public CardRulingItemProcessor
        (
            IWikiArticle wikiArticle, 
            ICardService cardService, 
            ICardRulingService cardRulingService
        )
        {
            _wikiArticle = wikiArticle;
            _cardService = cardService;
            _cardRulingService = cardRulingService;
        }

        public async Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item)
        {
            var response = new ArticleTaskResult { Article = item };

            var card = await _cardService.CardByName(item.Title);

            if (card != null)
            {
                var rulingSections = new List<CardRulingSection>();

                var articleCardRulings = await _wikiArticle.Simple(item.Id);

                foreach (var cardRulingSection in articleCardRulings.Sections)
                {
                    if(cardRulingSection.Title.Equals("References", StringComparison.OrdinalIgnoreCase))
                        continue;

                    var rulingSection = new CardRulingSection
                    {
                        Name = cardRulingSection.Title,
                        Rulings = GetSectionContentList(cardRulingSection)
                    };

                    rulingSections.Add(rulingSection);
                }

                await _cardRulingService.Update(card.Id, rulingSections);
            }

            return response;
        }

        private List<string> GetSectionContentList(Section cardTipSection)
        {
            var content = new List<string>();

            foreach (var c in cardTipSection.Content)
            {
                GetContentList(c.Elements, content);
            }

            return content;
        }

        public void GetContentList(IEnumerable<ListElement> elementList, List<string> contentlist)
        {
            if (elementList != null)
            {
                foreach (var e in elementList)
                {
                    if (e != null)
                    {
                        if (!string.IsNullOrEmpty(e.Text))
                            contentlist.Add(e.Text);

                        GetContentList(e.Elements, contentlist);
                    }
                }
            }
        }

        public bool Handles(string category)
        {
            return category == ArticleCategory.CardRulings;
        }
    }
}