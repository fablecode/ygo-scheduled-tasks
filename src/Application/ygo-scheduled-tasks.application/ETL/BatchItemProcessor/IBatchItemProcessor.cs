using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.application.ETL.BatchItemProcessor
{
    public interface IBatchItemProcessor
    {
        Task<Card> ProcessItem(UnexpandedArticle item);

        bool Handles(string category);
    }
}