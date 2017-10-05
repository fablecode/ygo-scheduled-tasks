using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;

namespace ygo_scheduled_tasks.domain.ETL.Processor
{
    public interface IArticleBatchProcessor
    {
        Task<ArticleBatchTaskResult> Process(string category, UnexpandedArticle[] articles);
    }
}