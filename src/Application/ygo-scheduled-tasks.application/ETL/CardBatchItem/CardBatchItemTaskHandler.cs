using System;
using System.Threading.Tasks;
using MediatR;
using ygo_scheduled_tasks.application.ETL.BatchHandler;
using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.application.ETL.CardBatchItem
{
    public class CardBatchItemTaskHandler : IAsyncRequestHandler<CardBatchItemTask, Card>
    {
        private readonly IBatchHandler _batchHandler;

        public CardBatchItemTaskHandler(IBatchHandler batchHandler)
        {
            _batchHandler = batchHandler;
        }
        public async Task<Card> Handle(CardBatchItemTask message)
        {
            var batchItemProcessor = _batchHandler.Handler(message.Category);

            var card = await batchItemProcessor.ProcessItem(message.Item);

            return card;
        }
    }
}