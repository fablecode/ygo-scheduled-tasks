using System.Threading.Tasks;
using NLog;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.core.Enums;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Handler;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;
using ygo_scheduled_tasks.domain.ETL.Banlist.DataSource;

namespace ygo_scheduled_tasks.domain.ETL.Banlist.Processor
{
    public class BanlistProcessor : IBanlistProcessor
    {
        private readonly IBanlistUrlDataSource _banlistUrlDataSource;
        private readonly IArticleHandler _articleHandler;
        private readonly ILogger _logger;

        public BanlistProcessor(IBanlistUrlDataSource banlistUrlDataSource, IArticleHandler articleHandler)
        {
            _banlistUrlDataSource = banlistUrlDataSource;
            _articleHandler = articleHandler;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<ArticleBatchTaskResult> Process(BanlistType banlistType)
        {
            var response = new ArticleBatchTaskResult();

            const string baseBanlistUrl = "http://yugioh.wikia.com/wiki/July_1999_Lists";
            var banListArticleIds = _banlistUrlDataSource.GetBanlists(banlistType, baseBanlistUrl);
            var articleItemProcessor = _articleHandler.Handler(ArticleCategory.ForbiddenAndLimited);

            foreach (var banListArticleId in banListArticleIds)
            {
                _logger.Info($"{banlistType.ToString().ToUpper()} banlists for the year: {banListArticleId.Key}");

                foreach (var articleId in banListArticleId.Value)
                {
                    _logger.Info($"{banlistType.ToString().ToUpper()} banlist articleId: {articleId}");

                    var articleResult = await articleItemProcessor.ProcessItem(new UnexpandedArticle {Id = articleId});

                    if (articleResult.IsSuccessfullyProcessed)
                        response.Processed += 1;
                }
            }

            return response;
        }
    }
}