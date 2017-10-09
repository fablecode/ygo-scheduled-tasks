namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Handler
{
    public interface ISemanticCardHandler
    {
        ISemanticCardItemProcess Handler(string category);
    }
}