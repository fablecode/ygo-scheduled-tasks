using System.Threading.Tasks;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor
{
    public interface ISemanticSearchProcessor
    {
        Task<SemanticSearchBatchTaskResult> ProcessUrl(string category, string url);
    }
}