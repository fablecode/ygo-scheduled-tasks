using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model
{
    public class SemanticSearchTaskResult
    {
        public bool IsSuccessfullyProcessed { get; set; }

        public SemanticCard Card { get; set; }

        public YugiohCard YugiohCard { get; set; }

        public SemanticSearchException Failed { get; set; }
    }
}