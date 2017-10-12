using System;
using System.Threading.Tasks;
using NLog;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor
{
    public class SemanticSearchBatchProcessor : ISemanticSearchBatchProcessor
    {
        private readonly ISemanticCardProcessor _semanticCardProcessor;
        private readonly Logger _logger;

        public SemanticSearchBatchProcessor(ISemanticCardProcessor semanticCardProcessor)
        {
            _semanticCardProcessor = semanticCardProcessor;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<SemanticSearchBatchTaskResult> Process(string category, SemanticCard[] semanticCards)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException(nameof(category));

            if (semanticCards == null)
                throw new ArgumentException(nameof(semanticCards));

            var response = new SemanticSearchBatchTaskResult();

            foreach (var semanticCard in semanticCards)
            {
                try
                {
                    var result = await _semanticCardProcessor.Process(category, semanticCard);

                    if (result.IsSuccessfullyProcessed)
                        response.Processed += 1;
                }
                catch (Exception ex)
                {
                    _logger.Error("{1} | ' {0} '", semanticCard.Name, category);
                    _logger.Error(ex);
                    response.Failed.Add(new SemanticSearchException { Card = semanticCard, Exception = ex });
                }
            }

            return response;
        }
    }
}