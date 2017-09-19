using MediatR;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.application.ETL.CardBatchItem
{
    public class CardBatchItemTask : IRequest<Card>
    {
        public string Category { get; set; }
        public UnexpandedArticle Item { get; set; }
    }
}