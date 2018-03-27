using System;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.Command;
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

            var articleUrl = _config.WikiaDomainUrl + item.Url;
            var archetypeUrl = new Uri(articleUrl);

            var existingArchetype = await _archetypeService.ArchetypeById(item.Id);
            var archetype = existingArchetype == null
                ? await _archetypeService.Add(new AddArchetypeCommand
                {
                    ArchetypeNumber = item.Id,
                    Name = item.Title,
                    Thumbnail = ArchetypeHelper.ExtractThumbnailUrl(articleUrl),
                    Url = archetypeUrl.AbsoluteUri,
                    Cards = _archetypeWebPage.Cards(archetypeUrl)
                })
                : await _archetypeService.Update(new UpdateArchetypeCommand
                {
                    Id = existingArchetype.Id,
                    Name = item.Title,
                    Thumbnail = ArchetypeHelper.ExtractThumbnailUrl(articleUrl),
                    Url = archetypeUrl.AbsoluteUri,
                    Cards = _archetypeWebPage.Cards(archetypeUrl)
                });


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