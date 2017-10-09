using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor
{
    public interface ISemanticSearchBatchProcessor
    {
        Task<SemanticSearchBatchTaskResult> Process(string category, SemanticCard[] semanticCards);
    }
}