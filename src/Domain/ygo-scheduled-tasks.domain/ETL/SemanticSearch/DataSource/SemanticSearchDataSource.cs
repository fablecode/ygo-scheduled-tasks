﻿using System;
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

        public void Producer(string url, ITargetBlock<SemanticCard[]> targetBlock)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(nameof(url));

            if (targetBlock == null)
                throw new ArgumentException(nameof(targetBlock));

            var cards = _semanticSearch.CardsByUrl(url);

            targetBlock.Post(cards.ToArray());
            targetBlock.Complete();
        }
    }
}