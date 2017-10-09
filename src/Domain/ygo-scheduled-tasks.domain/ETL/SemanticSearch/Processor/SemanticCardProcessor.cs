using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Handler;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor
{
    public class SemanticCardProcessor : ISemanticCardProcessor
    {
        private readonly ISemanticCardHandler _semanticCardHandler;

        public SemanticCardProcessor(ISemanticCardHandler semanticCardHandler)
        {
            _semanticCardHandler = semanticCardHandler;
        }

        public Task<SemanticSearchTaskResult> Process(string category, SemanticCard semanticCard)
        {
            var handler = _semanticCardHandler.Handler(category);

            return handler.ProcessItem(semanticCard);
        }
    }
}