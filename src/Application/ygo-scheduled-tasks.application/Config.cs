using System.Configuration;
using ygo_scheduled_tasks.domain;

namespace ygo_scheduled_tasks.application
{
    public class Config : ConfigurationSection, IConfig
    {
        private const string WikiaDomainUrlKey = "WikiaDomainUrl";
        private const string ApiUrlKey = "ApiUrl";
        private const string OAuthEmailKey = "OAuthEmail";
        private const string OAuthPasswordKey = "OAuthPassword";

        [ConfigurationProperty(WikiaDomainUrlKey, IsRequired = true)]
        public string WikiaDomainUrl => (string)this[WikiaDomainUrlKey];

        [ConfigurationProperty(ApiUrlKey, IsRequired = true)]
        public string ApiUrl => (string) this[ApiUrlKey];

        [ConfigurationProperty(OAuthEmailKey, IsRequired = true)]
        public string OAuthEmail => (string) this[OAuthEmailKey];

        [ConfigurationProperty(OAuthPasswordKey, IsRequired = true)]
        public string OAuthPassword => (string)this[OAuthPasswordKey];
    }
}