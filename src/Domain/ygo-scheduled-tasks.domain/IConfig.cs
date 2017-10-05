namespace ygo_scheduled_tasks.domain
{
    public interface IConfig
    {
        string WikiaDomainUrl { get; }
        string ApiUrl { get; }
        string OAuthEmail { get; }
        string OAuthPassword { get; }
    }
}