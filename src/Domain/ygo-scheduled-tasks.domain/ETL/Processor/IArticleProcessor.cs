using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;

namespace ygo_scheduled_tasks.domain.ETL.Processor
{
    public interface IArticleProcessor
    {
        Task<ArticleTaskResult> Process(string category, UnexpandedArticle article);
    }
}