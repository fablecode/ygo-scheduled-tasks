using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using wikia.Models.Article.AlphabeticalList;

namespace ygo_scheduled_tasks.application.ETL.DataSource
{
    public interface ICategoryDataSource
    {
        Task Producer(string category, int pageSize, ITargetBlock<UnexpandedArticle[]> targetBlock);
    }
}