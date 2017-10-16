using System;
using wikia.Models.Article.AlphabeticalList;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model
{
    public class ArticleException
    {
        public UnexpandedArticle Article { get; set; }

        public Exception Exception { get; set; }
    }
}