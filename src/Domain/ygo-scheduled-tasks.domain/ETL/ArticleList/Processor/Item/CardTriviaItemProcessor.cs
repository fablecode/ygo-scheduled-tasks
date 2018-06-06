using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wikia.Api;
using wikia.Models.Article.AlphabeticalList;
using wikia.Models.Article.Simple;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Helpers;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item
{
    public class CardTriviaItemProcessor : IArticleItemProcessor
    {
        private readonly IWikiArticle _wikiArticle;
        private readonly ICardService _cardService;
        private readonly ICardTriviaService _cardTriviaService;

        public CardTriviaItemProcessor
        (
            IWikiArticle wikiArticle, 
            ICardService cardService,
            ICardTriviaService cardTriviaService
        )
        {
            _wikiArticle = wikiArticle;
            _cardService = cardService;
            _cardTriviaService = cardTriviaService;
        }

        public async Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item)
        {
            var response = new ArticleTaskResult { Article = item };

            var card = await _cardService.CardByName(item.Title);

            if (card != null)
            {
                var triviaSections = new List<CardTriviaSection>();

                var articleCardTrivia = await _wikiArticle.Simple(item.Id);

                foreach (var cardTriviaSection in articleCardTrivia.Sections)
                {
                    if(cardTriviaSection.Title.Equals("References", StringComparison.OrdinalIgnoreCase))
                        continue;

                    var rulingSection = new CardTriviaSection
                    {
                        Name = cardTriviaSection.Title,
                        Trivia = SectionHelper.GetSectionContentList(cardTriviaSection)
                    };

                    triviaSections.Add(rulingSection);
                }

                await _cardTriviaService.Update(card.Id, triviaSections);
            }

            return response;
        }

        public bool Handles(string category)
        {
            return category == ArticleCategory.CardTrivia;
        }
    }
}