using System.Threading.Tasks;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.ETL.Processor
{
    public interface ICategoryProcessor
    {
        Task<ArticleBatchTaskResult> Process(string category, int pageSize);
    }
}