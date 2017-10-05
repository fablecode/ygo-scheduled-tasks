using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using wikia.Api;
using wikia.Models.Article;
using wikia.Models.Article.AlphabeticalList;

namespace ygo_scheduled_tasks.domain.ETL.DataSource
{
    public class ArticleCategoryDataSource : ICategoryDataSource
    {
        private readonly IWikiArticle _wikiArticle;

        public ArticleCategoryDataSource(IWikiArticle wikiArticle)
        {
            _wikiArticle = wikiArticle;
        }

        public async Task Producer(string category, int pageSize, ITargetBlock<UnexpandedArticle[]> targetBlock)
        {
            if(string.IsNullOrWhiteSpace(category))
                throw new ArgumentException(nameof(category));

            if(targetBlock == null)
                throw new ArgumentException(nameof(targetBlock));

            var nextBatch = await _wikiArticle.AlphabeticalList(new ArticleListRequestParameters { Category = category, Limit = pageSize });

            bool isNextBatchAvailable;

            do
            {
                targetBlock.Post(nextBatch.Items);

                isNextBatchAvailable = !string.IsNullOrEmpty(nextBatch.Offset);

                if (isNextBatchAvailable)
                {
                    nextBatch = await _wikiArticle.AlphabeticalList(new ArticleListRequestParameters
                    {
                        Category = category,
                        Limit = pageSize,
                        Offset = nextBatch.Offset
                    });
                }
            } while (isNextBatchAvailable);
        }
    }
}