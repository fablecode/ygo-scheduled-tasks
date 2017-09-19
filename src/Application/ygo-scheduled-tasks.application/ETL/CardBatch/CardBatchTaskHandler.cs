using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ygo_scheduled_tasks.application.ETL.CardBatchItem;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;
using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.application.ETL.CardBatch
{
    public class CardBatchTaskHandler : IAsyncRequestHandler<CardBatchTask, CategoryTaskResult>
    {
        private readonly IMediator _mediator;

        public CardBatchTaskHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CategoryTaskResult> Handle(CardBatchTask message)
        {
            var response = new CategoryTaskResult { Category = message.Category };

            foreach (var item in message.Items)
            {
                var card = await _mediator.Send(new CardBatchItemTask { Category = message.Category, Item = item });

                if (card == null)
                    continue;

                response.Processed++;
            }

            return response;
        }
    }
}