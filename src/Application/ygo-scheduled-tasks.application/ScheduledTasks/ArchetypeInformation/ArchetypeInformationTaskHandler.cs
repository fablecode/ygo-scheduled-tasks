using System.Threading.Tasks;
using MediatR;

namespace ygo_scheduled_tasks.application.ScheduledTasks.ArchetypeInformation
{
    public class ArchetypeInformationTaskHandler : IAsyncRequestHandler<ArchetypeInformationTask, ArchetypeInformationTaskResult>
    {
        public Task<ArchetypeInformationTaskResult> Handle(ArchetypeInformationTask message)
        {
            throw new System.NotImplementedException();
        }
    }
}