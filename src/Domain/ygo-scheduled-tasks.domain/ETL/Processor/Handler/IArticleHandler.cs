namespace ygo_scheduled_tasks.domain.ETL.Processor.Handler
{
    public interface IArticleHandler
    {
        IArticleItemProcessor Handler(string category);
    }
}