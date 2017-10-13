using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.DataSource
{
    public interface ISemanticSearchDataSource
    {
        void Producer(string url, ITargetBlock<SemanticCard[]> targetBlock);
    }
}