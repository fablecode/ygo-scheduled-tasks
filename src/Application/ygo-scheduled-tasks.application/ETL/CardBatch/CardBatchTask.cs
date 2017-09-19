using MediatR;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.ETL.CardBatch
{
    public class CardBatchTask : IRequest<CategoryTaskResult>
    {
        public string Category { get; set; }

        public UnexpandedArticle[] Items { get; set; }
    }
}