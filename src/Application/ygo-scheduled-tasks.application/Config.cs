using System.Configuration;

namespace ygo_scheduled_tasks.application
{
    public class Config : ConfigurationSection, IConfig
    {
        private const string WikiaDomainUrlKey = "WikiaDomainUrl";
        private const string ApiUrlKey = "ApiUrl";

        [ConfigurationProperty(WikiaDomainUrlKey, IsRequired = true)]
        public string WikiaDomainUrl => (string)this[WikiaDomainUrlKey];

        [ConfigurationProperty(ApiUrlKey, IsRequired = true)]
        public string ApiUrl => (string) this[ApiUrlKey];
    }
}