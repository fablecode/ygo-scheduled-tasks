using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor
{
    public interface ISemanticCardItemProcess
    {
        Task<SemanticSearchTaskResult> ProcessItem(SemanticCard semanticCard);
        bool Handles(string category);
    }
}