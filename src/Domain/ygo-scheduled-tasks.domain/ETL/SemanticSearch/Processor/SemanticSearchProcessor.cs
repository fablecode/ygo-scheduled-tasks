using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.DataSource;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor
{
    public class SemanticSearchProcessor : ISemanticSearchProcessor
    {
        private readonly ISemanticSearchDataSource _semanticSearchDataSource;
        private readonly ISemanticSearchBatchProcessor _semanticSearchBatchProcessor;

        public SemanticSearchProcessor(ISemanticSearchDataSource semanticSearchDataSource, ISemanticSearchBatchProcessor semanticSearchBatchProcessor)
        {
            _semanticSearchDataSource = semanticSearchDataSource;
            _semanticSearchBatchProcessor = semanticSearchBatchProcessor;
        }

        public Task<SemanticSearchBatchTaskResult> ProcessUrl(string category, string url)
        {
            var response = new SemanticSearchBatchTaskResult { Url = url };

            var processorCount = Environment.ProcessorCount;

            // Pipeline members
            var cardBatchBufferBlock = new BufferBlock<SemanticCard[]>();
            var cardTransformBlock = new TransformBlock<SemanticCard[], SemanticSearchBatchTaskResult>(semanticCards => _semanticSearchBatchProcessor.Process(category, semanticCards));
            var cardActionBlock = new ActionBlock<SemanticSearchBatchTaskResult>(delegate (SemanticSearchBatchTaskResult result)
                {
                    response.Processed += result.Processed;
                    response.Failed.AddRange(result.Failed);
                },
                // Specify a maximum degree of parallelism.
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = processorCount
                });

            // Form the pipeline
            cardBatchBufferBlock.LinkTo(cardTransformBlock);
            cardTransformBlock.LinkTo(cardActionBlock);

            //  Create the completion tasks:
            cardBatchBufferBlock.Completion
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        ((IDataflowBlock)cardTransformBlock).Fault(t.Exception);
                    else
                        cardTransformBlock.Complete();
                });

            cardTransformBlock.Completion
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        ((IDataflowBlock)cardActionBlock).Fault(t.Exception);
                    else
                        cardActionBlock.Complete();
                });

            // Process "Category" and generate article batch data
            _semanticSearchDataSource.Producer(url, cardBatchBufferBlock);

            // Mark the head of the pipeline as complete. The continuation tasks  
            // propagate completion through the pipeline as each part of the  
            // pipeline finishes.
            cardActionBlock.Completion.Wait();

            return Task.FromResult(response);
        }
    }
}