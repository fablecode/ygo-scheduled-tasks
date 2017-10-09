using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;

namespace ygo_scheduled_tasks.domain.ETL.Processor
{
    public interface IArticleItemProcessor
    {
        Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item);

        bool Handles(string category);
    }
}