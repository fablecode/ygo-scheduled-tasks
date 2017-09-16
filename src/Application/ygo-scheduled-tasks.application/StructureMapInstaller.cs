using System.Configuration;
using StructureMap;
using wikia.Api;

namespace ygo_scheduled_tasks.application
{
    public class StructureMapInstaller : Registry
    {
        public StructureMapInstaller()
        {
            var domainUrl = ConfigurationManager.AppSettings["domainUrl"];

            For<IWikiArticle>().Use(context => new WikiArticle(domainUrl));
        }
    }
}