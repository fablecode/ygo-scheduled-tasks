using System.Configuration;

namespace ygo_scheduled_tasks.application
{
    public class Config : ConfigurationSection, IConfig
    {
        private const string WikiaDomainUrlKey = "WikiaDomainUrl";

        [ConfigurationProperty(WikiaDomainUrlKey, IsRequired = true)]
        public string WikiaDomainUrl => (string)this[WikiaDomainUrlKey];
    }
}