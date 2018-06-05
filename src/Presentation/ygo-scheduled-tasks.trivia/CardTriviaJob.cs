using MediatR;
using Quartz;
using ygo_scheduled_tasks.application.ScheduledTasks.CardTrivia;

namespace ygo_scheduled_tasks.trivia
{
    public class CardTriviaJob : IJob
    {
        private readonly IMediator _mediator;

        public CardTriviaJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void Execute(IJobExecutionContext context)
        {
            const int pageSize = 500;
            const string category = "Card Trivia";

            await _mediator.Send(new CardTriviaTask { Category = category, PageSize = pageSize });
        }
    }
}