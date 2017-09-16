using FluentValidation;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardInformation
{
    public class CardInformationTaskValidator : AbstractValidator<CardInformationTask>
    {
        public CardInformationTaskValidator()
        {
            RuleFor(ci => ci.Categories)
                .NotNull()
                .NotEmpty();
        }
    }
}