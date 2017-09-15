using System;
using System.Threading.Tasks;
using MediatR;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardInformation
{
    public class CardInformationTaskHandler : IAsyncRequestHandler<CardInformationTask, ScheduleTaskResult>
    {
        public Task<ScheduleTaskResult> Handle(CardInformationTask message)
        {
            throw new NotImplementedException();
        }
    }
}