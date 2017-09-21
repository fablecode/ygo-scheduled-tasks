using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.ETL.Processor
{
    public interface IArticleProcessor
    {
        Task<ArticleTaskResult> Process(string category, UnexpandedArticle article);
    }
}