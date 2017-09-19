using System.Configuration;
using StructureMap;
using wikia.Api;
using ygo_scheduled_tasks.application.ETL.BatchItemProcessor;

namespace ygo_scheduled_tasks.application
{
    public class StructureMapInstaller : Registry
    {
        public StructureMapInstaller()
        {
            var domainUrl = ConfigurationManager.AppSettings["domainUrl"];

            Scan
            (
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.AddAllTypesOf<IBatchItemProcessor>();
                }
            );

            For<IWikiArticle>().Use(context => new WikiArticle(domainUrl));
        }
    }
}