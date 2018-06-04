using FluentValidation;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardRulings
{
    public class CardRulingsTaskValidator : AbstractValidator<CardRulingsTask>
    {
        public CardRulingsTaskValidator()
        {
            RuleFor(bl => bl.Category)
                .NotNull()
                .NotEmpty();

            RuleFor(bl => bl.PageSize)
                .GreaterThan(0);
        }
    }
}