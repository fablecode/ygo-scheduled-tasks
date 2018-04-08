using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;
using ygo_scheduled_tasks.domain.Helpers;
using ygo_scheduled_tasks.domain.Services;
using ygo_scheduled_tasks.domain.WebPage.Archetypes;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item
{
    public class CardsByArchetypeItemProcessor : IArticleItemProcessor
    {
        private readonly IArchetypeWebPage _archetypeWebPage;
        private readonly IArchetypeService _archetypeService;
        private readonly IArchetypeCardsService _archetypeCardsService;
        private readonly IConfig _config;

        public CardsByArchetypeItemProcessor
        (
            IArchetypeWebPage archetypeWebPage, 
            IArchetypeService archetypeService, 
            IArchetypeCardsService archetypeCardsService, 
            IConfig config
        )
        {
            _archetypeWebPage = archetypeWebPage;
            _archetypeService = archetypeService;
            _archetypeCardsService = archetypeCardsService;
            _config = config;
        }

        public async Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item)
        {
            var response = new ArticleTaskResult { Article = item };
            var archetypeName = StringHelpers.ArchetypeNameFromListTitle(item.Title);

            var archetypeUrl = new Uri(new Uri(_config.WikiaDomainUrl), item.Url);

            var existingArchetype = await _archetypeService.ArchetypeByName(archetypeName);

            if (existingArchetype != null)
            {
                var archetype = await _archetypeCardsService.Update(new UpdateArchetypeCardsCommand { ArchetypeId = existingArchetype.Id, Cards = _archetypeWebPage.Cards(archetypeUrl)});

                if (archetype != null)
                    response.IsSuccessfullyProcessed = true;
            }

            return response;
        }

        public bool Handles(string category)
        {
            return category == ArticleCategory.CardsByArchetype;
        }
    }
}