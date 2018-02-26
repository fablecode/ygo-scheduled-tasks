using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item
{
    public class ArchetypeSupportItemProcessor : IArticleItemProcessor
    {
        public Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item)
        {
            var archetypeToAddOrUpdated = new ArchetypeInputModel
            {
                Name = Regex.Matches(item.Title, "\"([^\"]*?)\"")[0].Groups[1].Value,
                Alias = item.Title,
                ArchetypeNumber = item.Id,
            };

            throw new NotImplementedException();
        }

        public bool Handles(string category)
        {
            return category == ArticleCategory.ArchetypeSupport;
        }
    }

    public class ArchetypeInputModel
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public int ArchetypeNumber { get; set; }
    }
}