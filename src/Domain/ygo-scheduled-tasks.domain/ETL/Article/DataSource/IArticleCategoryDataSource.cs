using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using wikia.Models.Article.AlphabeticalList;

namespace ygo_scheduled_tasks.domain.ETL.Article.DataSource
{
    public interface IArticleCategoryDataSource
    {
        Task Producer(string category, int pageSize, ITargetBlock<UnexpandedArticle[]> targetBlock);
    }
}