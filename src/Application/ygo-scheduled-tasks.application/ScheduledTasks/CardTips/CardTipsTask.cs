using MediatR;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardTips
{
    public class CardTipsTask : IRequest<CardTipsTaskResult>
    {
        public string Category { get; set; }
        public int PageSize { get; set; }
    }
}