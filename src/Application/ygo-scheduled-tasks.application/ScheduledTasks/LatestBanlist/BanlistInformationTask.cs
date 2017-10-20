using MediatR;

namespace ygo_scheduled_tasks.application.ScheduledTasks.LatestBanlist
{
    public class BanlistInformationTask : IRequest<BanlistInformationTaskResult>
    {
        public string Category { get; set; }

        public int PageSize { get; set; }
    }
}