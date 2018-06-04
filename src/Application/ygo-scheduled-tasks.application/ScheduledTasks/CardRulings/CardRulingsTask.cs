using MediatR;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardRulings
{
    public class CardRulingsTask : IRequest<CardRulingsTaskResult>
    {
        public string Category { get; set; }
        public int PageSize { get; set; }
    }
}