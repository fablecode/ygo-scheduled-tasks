using FluentValidation;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardTips
{
    public class CardTipsTaskValidator : AbstractValidator<CardTipsTask>
    {
        public CardTipsTaskValidator()
        {
            RuleFor(bl => bl.Category)
                .NotNull()
                .NotEmpty();

            RuleFor(bl => bl.PageSize)
                .GreaterThan(0);
        }
    }
}