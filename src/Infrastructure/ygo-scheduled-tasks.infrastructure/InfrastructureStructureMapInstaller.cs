using StructureMap;
using ygo_scheduled_tasks.application.Api;
using ygo_scheduled_tasks.application.Client;
using ygo_scheduled_tasks.infrastructure.Api;
using ygo_scheduled_tasks.infrastructure.Client;

namespace ygo_scheduled_tasks.infrastructure
{
    public class InfrastructureStructureMapInstaller : Registry
    {
        public InfrastructureStructureMapInstaller()
        {
            Scan
            (
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.AssembliesFromApplicationBaseDirectory();
                }
            );

            For<ICardService>().Use<CardService>();
            For<ICategoryService>().Use<CategoryService>();
            For<ISubCategoryService>().Use<SubCategoryService>();
            For<ITypeService>().Use<TypeService>();
            For<IAttributeService>().Use<AttributeService>();
            For<ILinkArrowService>().Use<LinkArrowService>();
            For<IYugiohCardService>().Use<YugiohCardService>();

            For<IRestClient>().Use<RestClient>();
        }
    }
}