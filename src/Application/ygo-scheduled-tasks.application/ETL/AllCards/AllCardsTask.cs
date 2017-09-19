using MediatR;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.ETL.AllCards
{
    public class AllCardsTask : IRequest<CategoryTaskResult>
    {
        public string Category { get; set; }

        public int PageSize { get; set; }
    }
}