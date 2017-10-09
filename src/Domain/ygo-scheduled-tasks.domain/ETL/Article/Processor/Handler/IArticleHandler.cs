namespace ygo_scheduled_tasks.domain.ETL.Article.Processor.Handler
{
    public interface IArticleHandler
    {
        IArticleItemProcessor Handler(string category);
    }
}