using System.Collections.Generic;
using MediatR;
using Quartz;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.cardinformation
{
    public class CardInformationJob : IJob
    {
        private readonly IMediator _mediator;

        public CardInformationJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Execute(IJobExecutionContext context)
        {
            const int pageSize = 500;
            const string tcgCards = "TCG cards";
            const string ocgCards = "OCG cards";

            var categories = new List<string> { tcgCards, ocgCards};

            _mediator.Send(new CardInformationTask { Categories = categories, PageSize = pageSize }).Wait();
        }
    }
}