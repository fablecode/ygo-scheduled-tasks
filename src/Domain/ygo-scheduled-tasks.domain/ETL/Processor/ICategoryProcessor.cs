using System.Threading.Tasks;

namespace ygo_scheduled_tasks.domain.ETL.Processor
{
    public interface ICategoryProcessor
    {
        Task<ArticleBatchTaskResult> Process(string category, int pageSize);
    }
}