using MediatR;
using Quartz;
using ygo_scheduled_tasks.application.ScheduledTasks.CardTips;

namespace ygo_scheduled_tasks.tips
{
    public class CardTipsJob : IJob
    {
        private readonly IMediator _mediator;

        public CardTipsJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void Execute(IJobExecutionContext context)
        {
            const int pageSize = 500;
            const string category = "Card Tips";

            await _mediator.Send(new CardTipsTask { Category = category, PageSize = pageSize });
        }
    }
}