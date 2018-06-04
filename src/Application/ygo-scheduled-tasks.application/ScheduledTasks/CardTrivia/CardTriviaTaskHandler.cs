using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardTrivia
{
    public class CardTriviaTaskHandler : IRequestHandler<CardTriviaTask, CardTriviaTaskResult>
    {
        private readonly IArticleCategoryProcessor _articleCategoryProcessor;
        private readonly IValidator<CardTriviaTask> _validator;

        public CardTriviaTaskHandler
        (
            IArticleCategoryProcessor articleCategoryProcessor, 
            IValidator<CardTriviaTask> validator
        )
        {
            _articleCategoryProcessor = articleCategoryProcessor;
            _validator = validator;
        }

        public async Task<CardTriviaTaskResult> Handle(CardTriviaTask request, CancellationToken cancellationToken)
        {
            var response = new CardTriviaTaskResult();

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