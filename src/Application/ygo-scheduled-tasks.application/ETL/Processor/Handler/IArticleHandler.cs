namespace ygo_scheduled_tasks.application.ETL.Processor.Handler
{
    public interface IArticleHandler
    {
        IBatchItemProcessor Handler(string category);
    }
}