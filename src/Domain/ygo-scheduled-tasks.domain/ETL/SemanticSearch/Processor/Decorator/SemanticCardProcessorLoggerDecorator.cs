using System.Threading.Tasks;
using NLog;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Decorator
{
    public class SemanticCardProcessorLoggerDecorator : ISemanticCardProcessor
    {
        private readonly ISemanticCardProcessor _semanticCardProcessor;
        private readonly Logger _logger;

        public SemanticCardProcessorLoggerDecorator(ISemanticCardProcessor semanticCardProcessor)
        {
            _semanticCardProcessor = semanticCardProcessor;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public Task<SemanticSearchTaskResult> Process(string category, SemanticCard semanticCard)
        {
            _logger.Info("{1} | ' {0} '", semanticCard.Name, category);
            return _semanticCardProcessor.Process(category, semanticCard);
        }
    }
}