namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Handler
{
    public interface IArticleHandler
    {
        IArticleItemProcessor Handler(string category);
    }
}