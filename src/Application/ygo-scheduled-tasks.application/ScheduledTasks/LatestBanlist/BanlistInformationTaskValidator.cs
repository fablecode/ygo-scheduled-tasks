using FluentValidation;

namespace ygo_scheduled_tasks.application.ScheduledTasks.LatestBanlist
{
    public class BanlistInformationTaskValidator : AbstractValidator<BanlistInformationTask>
    {
        public BanlistInformationTaskValidator()
        {
            RuleFor(ci => ci.Category)
                .NotNull()
                .NotEmpty();
        }
    }
}