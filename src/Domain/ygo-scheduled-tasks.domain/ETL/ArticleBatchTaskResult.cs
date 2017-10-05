using System.Collections.Generic;

namespace ygo_scheduled_tasks.domain.ETL
{
    public class ArticleBatchTaskResult
    {
        public string Category { get; set; }

        public int Processed { get; set; }

        public List<ArticleException> Failed { get; set; } = new List<ArticleException>();
    }
}