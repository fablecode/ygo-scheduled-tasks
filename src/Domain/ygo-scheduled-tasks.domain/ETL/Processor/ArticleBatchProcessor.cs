using System;
using System.Threading.Tasks;
using NLog;
using wikia.Models.Article.AlphabeticalList;

namespace ygo_scheduled_tasks.domain.ETL.Processor
{
    public class ArticleBatchProcessor : IArticleBatchProcessor
    {
        private readonly IArticleProcessor _articleProcessor;
        private ILogger _logger;

        public ArticleBatchProcessor(IArticleProcessor articleProcessor)
        {
            _articleProcessor = articleProcessor;
            _logger = LogManager.GetCurrentClassLogger();
        }
        public async Task<ArticleBatchTaskResult> Process(string category, UnexpandedArticle[] articles)
        {
            if(string.IsNullOrWhiteSpace(category))
                throw new ArgumentException(nameof(category));

            if(articles == null)
                throw new ArgumentException(nameof(articles));

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
                    _logger.Error("{1} | ' {0} '", article.Title, category);
                    response.Failed.Add(new ArticleException { Article = article, Exception = ex});
                }
            }

            return response;
        }
    }
}