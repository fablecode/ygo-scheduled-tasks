using System.Collections.Generic;
using System.Linq;
using ygo_scheduled_tasks.application.ETL.BatchItemProcessor;

namespace ygo_scheduled_tasks.application.ETL.BatchHandler
{
    public class BatchHandler : IBatchHandler
    {
        private readonly IEnumerable<IBatchItemProcessor> _batchItemProcessors;

        public BatchHandler(IEnumerable<IBatchItemProcessor> batchItemProcessors)
        {
            _batchItemProcessors = batchItemProcessors;
        }

        public IBatchItemProcessor Handler(string category)
        {
            return _batchItemProcessors.Single(h => h.Handles(category));
        }
    }
}