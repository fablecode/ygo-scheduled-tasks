using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor
{
    public interface IArticleItemProcessor
    {
        Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item);

        bool Handles(string category);
    }
}