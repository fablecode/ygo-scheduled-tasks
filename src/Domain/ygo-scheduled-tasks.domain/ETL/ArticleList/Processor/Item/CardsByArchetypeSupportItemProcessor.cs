using System;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;
using ygo_scheduled_tasks.domain.Helpers;
using ygo_scheduled_tasks.domain.Services;
using ygo_scheduled_tasks.domain.WebPage.Archetypes;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item
{
    public class CardsByArchetypeSupportItemProcessor : IArticleItemProcessor
    {
        private readonly IArchetypeWebPage _archetypeWebPage;
        private readonly IArchetypeService _archetypeService;
        private readonly IArchetypeSupportCardsService _archetypeSupportCardsService;
        private readonly IConfig _config;

        public CardsByArchetypeSupportItemProcessor
        (
            IArchetypeWebPage archetypeWebPage,
            IArchetypeService archetypeService,
            IArchetypeSupportCardsService archetypeSupportCardsService,
            IConfig config
        )
        {
            _archetypeWebPage = archetypeWebPage;
            _archetypeService = archetypeService;
            _archetypeSupportCardsService = archetypeSupportCardsService;
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
                var archetype = await _archetypeSupportCardsService.Update(new UpdateArchetypeSupportCardsCommand { ArchetypeId = existingArchetype.Id, Cards = _archetypeWebPage.Cards(archetypeUrl) });

                if (archetype != null)
                    response.IsSuccessfullyProcessed = true;
            }

            return response;
        }


        public bool Handles(string category)
        {
            return category == ArticleCategory.CardsByArchetypeSupport;
        }
    }
}