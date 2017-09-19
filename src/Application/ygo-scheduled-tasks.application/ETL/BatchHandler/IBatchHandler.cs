using ygo_scheduled_tasks.application.ETL.BatchItemProcessor;

namespace ygo_scheduled_tasks.application.ETL.BatchHandler
{
    public interface IBatchHandler
    {
        IBatchItemProcessor Handler(string category);
    }
}