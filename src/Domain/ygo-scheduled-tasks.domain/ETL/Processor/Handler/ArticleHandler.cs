using System.Collections.Generic;
using System.Linq;

namespace ygo_scheduled_tasks.domain.ETL.Processor.Handler
{
    public class ArticleHandler : IArticleHandler
    {
        private readonly IEnumerable<IArticleItemProcessor> _articleItemProcessors;

        public ArticleHandler(IEnumerable<IArticleItemProcessor> articleItemProcessors)
        {
            _articleItemProcessors = articleItemProcessors;
        }

        public IArticleItemProcessor Handler(string category)
        {
            return _articleItemProcessors.Single(h => h.Handles(category));
        }
    }
}