using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model
{
    public class ArticleTaskResult
    {
        public bool IsSuccessfullyProcessed { get; set; }

        public UnexpandedArticle Article { get; set; }

        public ArticleException Failed { get; set; }

        public object Data { get; set; }
    }
}