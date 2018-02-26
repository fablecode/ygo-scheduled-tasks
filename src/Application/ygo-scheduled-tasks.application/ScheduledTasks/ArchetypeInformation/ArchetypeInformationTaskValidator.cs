using System.Linq;
using FluentValidation;

namespace ygo_scheduled_tasks.application.ScheduledTasks.ArchetypeInformation
{
    public class ArchetypeInformationTaskValidator : AbstractValidator<ArchetypeInformationTask>
    {
        public ArchetypeInformationTaskValidator()
        {
            RuleFor(ci => ci.Categories)
                .NotNull()
                .NotEmpty()
                .Must(ci => ci.All(c => !string.IsNullOrWhiteSpace(c)))
                    .WithMessage("All {PropertyName} must be valid.");

            RuleFor(ci => ci.PageSize)
                .GreaterThan(0);
        }
    }
}