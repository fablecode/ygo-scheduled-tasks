using System.Threading.Tasks;
using MediatR;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.ScheduledTasks.AllCards
{
    public class AllCardsHandler : IAsyncRequestHandler<AllCardsTask, CategoryTaskResult>
    {
        public Task<CategoryTaskResult> Handle(AllCardsTask message)
        {
            throw new System.NotImplementedException();
        }
    }
}