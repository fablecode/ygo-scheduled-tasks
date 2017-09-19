using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo_scheduled_tasks.application.ETL.AllCards;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardInformation
{
    public class CardInformationTaskHandler : IAsyncRequestHandler<CardInformationTask, CardInformationTaskResult>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CardInformationTask> _validator;

        public CardInformationTaskHandler(IMediator mediator, IValidator<CardInformationTask> validator)
        {
            _mediator = mediator;
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
                    var categoryResult = await _mediator.Send(new AllCardsTask {Category = category, PageSize = message.PageSize});

                    response.CategoryTaskResults.Add(categoryResult);
                }
            }
            else
            {
                response.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return response;
        }
    }

    public class CardInformationTaskResult
    {
        public List<CategoryTaskResult> CategoryTaskResults { get; set; } = new List<CategoryTaskResult>();

        public List<string> Errors { get; set; }
    }

    public class CategoryTaskResult
    {
        public string Category { get; set; }

        public int Processed { get; set; }

        public List<CardException> Failed { get; set; }
    }

    public class CardException
    {
        public string Name { get; set; }

        public Exception Exception { get; set; }
    }
}