using System;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using ygo_scheduled_tasks.core.Enums;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;
using ygo_scheduled_tasks.domain.ETL.Banlist.Processor;

namespace ygo_scheduled_tasks.application.ScheduledTasks.LatestBanlist
{
    public class BanlistInformationTaskHandler : IRequestHandler<BanlistInformationTask, BanlistInformationTaskResult>
    {
        private readonly IArticleCategoryProcessor _articleCategoryProcessor;
        private readonly IValidator<BanlistInformationTask> _validator;
        private readonly IBanlistProcessor _banlistProcessor;
        private readonly ILogger _logger;


        public BanlistInformationTaskHandler
        (
            IArticleCategoryProcessor articleCategoryProcessor, 
            IValidator<BanlistInformationTask> validator,
            IBanlistProcessor banlistProcessor
        )
        {
            _articleCategoryProcessor = articleCategoryProcessor;
            _validator = validator;
            _banlistProcessor = banlistProcessor;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<BanlistInformationTaskResult> Handle(BanlistInformationTask request, CancellationToken cancellationToken)
        {
            var response = new BanlistInformationTaskResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {

                try
                {
                    _logger.Info("Banlist by category.....");
                    var categoryResult = await _articleCategoryProcessor.Process(request.Category, request.PageSize);

                    _logger.Info("Tcg Banlist by articleId.....");
                    var tcgdResult = await _banlistProcessor.Process(BanlistType.Tcg);

                    _logger.Info("Ocg Banlist by articleId.....");
                    var ocgResult = await _banlistProcessor.Process(BanlistType.Ocg);

                    response.ArticleTaskResults = categoryResult;
                    _logger.Info("Banlists tasks complete.....");

                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    throw;
                }
            }
            else
            {
                response.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return response;
        }
    }
}