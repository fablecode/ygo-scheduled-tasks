using System;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.ETL.Processor
{
    public class ArticleBatchProcessor : IArticleBatchProcessor
    {
        private readonly IArticleProcessor _articleProcessor;

        public ArticleBatchProcessor(IArticleProcessor articleProcessor)
        {
            _articleProcessor = articleProcessor;
        }
        public async Task<ArticleBatchTaskResult> Process(string category, UnexpandedArticle[] articles)
        {
            var response = new ArticleBatchTaskResult();

            foreach (var article in articles)
            {
                try
                {
                    var result = await _articleProcessor.Process(category, article);

                    if(result.Processed)
                        response.Processed += 1;
                }
                catch (Exception ex)
                {
                    response.Failed.Add(new ArticleException { Article = article, Exception = ex});
                }
            }

            return response;
        }
    }
}