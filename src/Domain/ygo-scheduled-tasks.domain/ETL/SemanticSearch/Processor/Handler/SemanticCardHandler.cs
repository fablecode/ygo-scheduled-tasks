using System.Collections.Generic;
using System.Linq;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Handler
{
    public class SemanticCardHandler : ISemanticCardHandler
    {
        private readonly IEnumerable<ISemanticCardItemProcess> _semanticCardItemProcessors;

        public SemanticCardHandler(IEnumerable<ISemanticCardItemProcess> semanticCardItemProcessors)
        {
            _semanticCardItemProcessors = semanticCardItemProcessors;
        }

        public ISemanticCardItemProcess Handler(string category)
        {
            return _semanticCardItemProcessors.Single(h => h.Handles(category));
        }
    }
}