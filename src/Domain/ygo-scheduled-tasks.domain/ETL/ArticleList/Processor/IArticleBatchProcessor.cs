using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor
{
    public interface IArticleBatchProcessor
    {
        Task<ArticleBatchTaskResult> Process(string category, UnexpandedArticle[] articles);
    }
}