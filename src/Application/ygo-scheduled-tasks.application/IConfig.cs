namespace ygo_scheduled_tasks.application
{
    public interface IConfig
    {
        string WikiaDomainUrl { get; }
        string ApiUrl { get; }
        string OAuthEmail { get; }
        string OAuthPassword { get; }
    }
}