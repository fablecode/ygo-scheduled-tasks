using System.Collections.Generic;

namespace ygo_scheduled_tasks.application.ETL.Processor.Model
{
    public class CategoryTaskItemResult
    {
        public string Category { get; set; }

        public int Processed { get; set; }

        public List<ArticleException> Failed { get; set; }
    }
}