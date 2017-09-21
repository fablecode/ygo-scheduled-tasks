using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.ETL.Processor
{
    public interface IBatchItemProcessor
    {
        Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item);

        bool Handles(string category);
    }
}