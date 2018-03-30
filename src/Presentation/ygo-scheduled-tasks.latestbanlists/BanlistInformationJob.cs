using System.Threading.Tasks;
using MediatR;
using Quartz;
using ygo_scheduled_tasks.application.ScheduledTasks.LatestBanlist;

namespace ygo_scheduled_tasks.latestbanlists
{
    public class BanlistInformationJob : IJob
    {
        private readonly IMediator _mediator;

        public BanlistInformationJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        Task IJob.Execute(IJobExecutionContext context)
        {
            const int pageSize = 500;
            var category = "Forbidden & Limited Lists";

            return _mediator.Send(new BanlistInformationTask { Category = category, PageSize = pageSize });
        }
    }
}