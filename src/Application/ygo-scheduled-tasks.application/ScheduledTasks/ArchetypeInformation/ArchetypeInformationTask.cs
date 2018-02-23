using MediatR;

namespace ygo_scheduled_tasks.application.ScheduledTasks.ArchetypeInformation
{
    public class ArchetypeInformationTask : IRequest<ArchetypeInformationTaskResult>
    {
        public string[] Categories { get; set; }

        public int PageSize { get; set; }
    }
}