using System.Configuration;

namespace ygo_scheduled_tasks.application
{
    public class Config : ConfigurationSection, IConfig
    {
        private const string DomainUrlKey = "DomainUrl";

        [ConfigurationProperty(DomainUrlKey, IsRequired = true)]
        public string DomainUrl => (string)this[DomainUrlKey];
    }
}