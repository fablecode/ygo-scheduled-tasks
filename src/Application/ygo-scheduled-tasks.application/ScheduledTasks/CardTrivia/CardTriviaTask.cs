using MediatR;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardTrivia
{
    public class CardTriviaTask : IRequest<CardTriviaTaskResult>
    {
        public string Category { get; set; }
        public int PageSize { get; set; }
    }
}