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

        public ArchetypeItemProcessor(IArchetypeWebPage archetypeWebPage, IArchetypeService archetypeService)
        {
            _archetypeWebPage = archetypeWebPage;
            _archetypeService = archetypeService;
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

            archetypeToAddOrUpdated.Cards = _archetypeWebPage.Cards(item.Url);

            var archetype = await _archetypeService.AddOrUpdate(archetypeToAddOrUpdated);

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