using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardRulings
{
    public class CardRulingsTaskHandler : IRequestHandler<CardRulingsTask, CardRulingsTaskResult>
    {
        private readonly IArticleCategoryProcessor _articleCategoryProcessor;
        private readonly IValidator<CardRulingsTask> _validator;

        public CardRulingsTaskHandler
        (
            IArticleCategoryProcessor articleCategoryProcessor, 
            IValidator<CardRulingsTask> validator
        )
        {
            _articleCategoryProcessor = articleCategoryProcessor;
            _validator = validator;
        }

        public async Task<CardRulingsTaskResult> Handle(CardRulingsTask request, CancellationToken cancellationToken)
        {
            var response = new CardRulingsTaskResult();

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