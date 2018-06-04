using FluentValidation;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardTrivia
{
    public class CardTriviaTaskValidator : AbstractValidator<CardTriviaTask>
    {
        public CardTriviaTaskValidator()
        {
            RuleFor(bl => bl.Category)
                .NotNull()
                .NotEmpty();

            RuleFor(bl => bl.PageSize)
                .GreaterThan(0);
        }
    }
}