using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;

namespace ygo_scheduled_tasks.application.ScheduledTasks.LatestBanlist
{
    public class BanlistInformationTaskHandler : IRequestHandler<BanlistInformationTask, BanlistInformationTaskResult>
    {
        private readonly IArticleCategoryProcessor _articleCategoryProcessor;
        private readonly IValidator<BanlistInformationTask> _validator;

        public BanlistInformationTaskHandler(IArticleCategoryProcessor articleCategoryProcessor, IValidator<BanlistInformationTask> validator)
        {
            _articleCategoryProcessor = articleCategoryProcessor;
            _validator = validator;
        }

        public async Task<BanlistInformationTaskResult> Handle(BanlistInformationTask request, CancellationToken cancellationToken)
        {
            var response = new BanlistInformationTaskResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var categoryResult = await _articleCategoryProcessor.Process(request.Category, request.PageSize);

                response.ArticleTaskResults = categoryResult;
            }
            else
            {
                response.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return response;
        }
    }
}