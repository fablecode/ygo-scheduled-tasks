using System.Threading.Tasks;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor
{
    public interface IArticleCategoryProcessor
    {
        Task<ArticleBatchTaskResult> Process(string category, int pageSize);
    }
}