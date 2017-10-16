using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Handler;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor
{
    public class ArticleProcessor : IArticleProcessor
    {
        private readonly IArticleHandler _articleHandler;

        public ArticleProcessor(IArticleHandler articleHandler)
        {
            _articleHandler = articleHandler;
        }

        public Task<ArticleTaskResult> Process(string category, UnexpandedArticle article)
        {
            var handler = _articleHandler.Handler(category);

            return handler.ProcessItem(article);
        }
    }
}