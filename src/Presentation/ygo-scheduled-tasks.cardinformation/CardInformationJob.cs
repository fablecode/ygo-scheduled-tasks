using System.Collections.Generic;
using MediatR;
using Quartz;
using ygo_scheduled_tasks.application.ETL;
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
            var categories = new List<string> { ArticleCategory.TcgCards, ArticleCategory.OcgCards, ArticleCategory.MonsterCardTypes};

            _mediator.Send(new CardInformationTask { Categories = categories, PageSize = pageSize }).Wait();
        }
    }
}