using MediatR;
using Quartz;
using ygo_scheduled_tasks.application.ScheduledTasks.CardRulings;

namespace ygo_scheduled_tasks.rulings
{
    public class CardRulingsJob : IJob
    {
        private readonly IMediator _mediator;

        public CardRulingsJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void Execute(IJobExecutionContext context)
        {
            const int pageSize = 500;
            const string category = "Card Rulings";

            await _mediator.Send(new CardRulingsTask { Category = category, PageSize = pageSize });
        }
    }
}