using System.Collections.Generic;
using System.Linq;

namespace ygo_scheduled_tasks.application.ETL.Processor.Handler
{
    public class ArticleHandler : IArticleHandler
    {
        private readonly IEnumerable<IBatchItemProcessor> _batchItemProcessors;

        public ArticleHandler(IEnumerable<IBatchItemProcessor> batchItemProcessors)
        {
            _batchItemProcessors = batchItemProcessors;
        }

        public IBatchItemProcessor Handler(string category)
        {
            return _batchItemProcessors.Single(h => h.Handles(category));
        }
    }
}