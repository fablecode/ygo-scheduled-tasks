using MediatR;
using Quartz;
using ygo_scheduled_tasks.application.ScheduledTasks.ArchetypeInformation;

namespace ygo_scheduled_tasks.archetypes
{
    public class ArchetypeInformationJob : IJob
    {
        private readonly IMediator _mediator;

        public ArchetypeInformationJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void Execute(IJobExecutionContext context)
        {
            const int pageSize = 500;
            var categories = new[] { "Cards by archetype", "Cards by archetype support" };

            await _mediator.Send(new ArchetypeInformationTask { Categories = categories, PageSize = pageSize });
        }
    }
}