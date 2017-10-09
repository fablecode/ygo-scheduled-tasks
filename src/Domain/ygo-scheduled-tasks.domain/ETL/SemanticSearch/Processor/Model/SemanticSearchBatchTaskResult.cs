using System.Collections.Generic;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model
{
    public class SemanticSearchBatchTaskResult
    {
        public string Url { get; set; }

        public int Processed { get; set; }

        public List<SemanticSearchException> Failed { get; set; } = new List<SemanticSearchException>();
    }
}