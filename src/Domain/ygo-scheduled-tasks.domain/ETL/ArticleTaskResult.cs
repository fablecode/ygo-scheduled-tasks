using wikia.Models.Article.AlphabeticalList;

namespace ygo_scheduled_tasks.domain.ETL
{
    public class ArticleTaskResult
    {
        public bool Processed { get; set; }

        public UnexpandedArticle Article { get; set; }

        public ArticleException Failed { get; set; }
    }
}