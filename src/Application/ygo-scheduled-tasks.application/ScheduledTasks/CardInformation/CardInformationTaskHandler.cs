using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo_scheduled_tasks.domain.ETL.Processor;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardInformation
{
    public class CardInformationTaskHandler : IAsyncRequestHandler<CardInformationTask, CardInformationTaskResult>
    {
        private readonly ICategoryProcessor _categoryProcessor;
        private readonly IValidator<CardInformationTask> _validator;

        public CardInformationTaskHandler(ICategoryProcessor categoryProcessor, IValidator<CardInformationTask> validator)
        {
            _categoryProcessor = categoryProcessor;
            _validator = validator;
        }

        public async Task<CardInformationTaskResult> Handle(CardInformationTask message)
        {
            var response = new CardInformationTaskResult();

            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                foreach (var category in message.Categories)
                {
                    var categoryResult = await _categoryProcessor.Process(category, message.PageSize);

                    response.ArticleTaskResults.Add(categoryResult);
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