using System;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;
using ygo_scheduled_tasks.domain.Helpers;
using ygo_scheduled_tasks.domain.Services;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item
{
    public class ArchetypeItemProcessor : IArticleItemProcessor
    {
        private readonly IArchetypeWebPage _archetypeWebPage;
        private readonly IArchetypeService _archetypeService;
        private readonly IConfig _config;

        public ArchetypeItemProcessor(IArchetypeWebPage archetypeWebPage, IArchetypeService archetypeService, IConfig config)
        {
            _archetypeWebPage = archetypeWebPage;
            _archetypeService = archetypeService;
            _config = config;
        }

        public async Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item)
        {
            var response = new ArticleTaskResult { Article = item };

            var archetypeToAddOrUpdated = new YugiohArchetype
            {
                Name = ArchetypeHelper.ExtractArchetypeName(item.Title),
                Alias = item.Title,
                ArchetypeNumber = item.Id,
            };

            var archetypeUrl = new Uri(_config.WikiaDomainUrl + item.Url);

            archetypeToAddOrUpdated.Cards = _archetypeWebPage.Cards(archetypeUrl);

            var existingArchetype = _archetypeService.ArchetypeByName(archetypeToAddOrUpdated.Name);

            var archetype = existingArchetype == null
                ? await _archetypeService.Add(archetypeToAddOrUpdated)
                : await _archetypeService.Update(archetypeToAddOrUpdated);

            if (archetype != null)
                response.IsSuccessfullyProcessed = true;

            return response;
        }

        public bool Handles(string category)
        {
            return category == ArticleCategory.Archetype;
        }
    }
}