using System.Threading.Tasks;
using NLog;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL.Article.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.Article.Processor.Decorator
{
    public class ArticleProcessLoggerDecorator : IArticleProcessor
    {
        private readonly IArticleProcessor _articleProcessor;
        private readonly ILogger _logger;

        public ArticleProcessLoggerDecorator(IArticleProcessor articleProcessor)
        {
            _articleProcessor = articleProcessor;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public Task<ArticleTaskResult> Process(string category, UnexpandedArticle article)
        {
            _logger.Info("{1} | ' {0} '", article.Title, category);
            return _articleProcessor.Process(category, article);
        }
    }
}