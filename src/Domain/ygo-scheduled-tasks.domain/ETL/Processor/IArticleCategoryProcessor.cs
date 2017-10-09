using System.Threading.Tasks;

namespace ygo_scheduled_tasks.domain.ETL.Processor
{
    public interface IArticleCategoryProcessor
    {
        Task<ArticleBatchTaskResult> Process(string category, int pageSize);
    }
}