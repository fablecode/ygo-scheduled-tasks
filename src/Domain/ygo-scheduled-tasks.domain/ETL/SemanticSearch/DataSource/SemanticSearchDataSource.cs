using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.core.WebPage;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.DataSource
{
    public class SemanticSearchDataSource : ISemanticSearchDataSource
    {
        private readonly ISemanticSearch _semanticSearch;

        public SemanticSearchDataSource(ISemanticSearch semanticSearch)
        {
            _semanticSearch = semanticSearch;
        }

        public async Task Producer(string url, ITargetBlock<SemanticCard[]> targetBlock)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(nameof(url));

            if (targetBlock == null)
                throw new ArgumentException(nameof(targetBlock));

            var cards = _semanticSearch.CardsByUrl(url);

            await Task.FromResult(targetBlock.Post(cards.ToArray()));
        }
    }
}