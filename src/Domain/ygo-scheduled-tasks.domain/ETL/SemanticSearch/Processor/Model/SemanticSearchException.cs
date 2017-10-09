using System;
using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model
{
    public class SemanticSearchException
    {
        public SemanticCard Card { get; set; }

        public Exception Exception { get; set; }
    }
}