using FluentValidation;

namespace ygo_scheduled_tasks.application.ScheduledTasks.ArchetypeInformation
{
    public class ArchetypeInformationTaskValidator : AbstractValidator<ArchetypeInformationTask>
    {
        public ArchetypeInformationTaskValidator()
        {
            RuleFor(ci => ci.Categories)
                .NotNull()
                .NotEmpty();

            RuleFor(ci => ci.PageSize)
                .GreaterThan(0);
        }
    }
}