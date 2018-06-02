using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;
using ygo_scheduled_tasks.domain.ETL.Banlist.Processor;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardTips
{
    public class CardTipsTaskHandler : IRequestHandler<CardTipsTask, CardTipsTaskResult>
    {
        private readonly IArticleCategoryProcessor _articleCategoryProcessor;
        private readonly IValidator<CardTipsTask> _validator;

        public CardTipsTaskHandler
        (
            IArticleCategoryProcessor articleCategoryProcessor, 
            IValidator<CardTipsTask> validator
        )
        {
            _articleCategoryProcessor = articleCategoryProcessor;
            _validator = validator;
        }

        public async Task<CardTipsTaskResult> Handle(CardTipsTask request, CancellationToken cancellationToken)
        {
            var response = new CardTipsTaskResult();

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