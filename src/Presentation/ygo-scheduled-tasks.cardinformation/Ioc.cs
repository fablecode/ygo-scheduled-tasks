using MediatR;
using StructureMap;
using StructureMap.Graph;

namespace ygo_scheduled_tasks.cardinformation
{
    public static class Ioc
    {
        public static Container Initialize()
        {
            var container = new Container(cfg =>
            {
                cfg.Scan(

                    scan =>
                    {
                        scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>)); // Handlers with no response
                        scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>)); // Handlers with a response
                        scan.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<>)); // Async handlers with no response
                        scan.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>)); // Async Handlers with a response
                        scan.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                        scan.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));

                        scan.TheCallingAssembly();
                        scan.WithDefaultConventions();
                        scan.AssembliesFromApplicationBaseDirectory();
                        scan.LookForRegistries();
                    });
            });

            return container;
        }
    }
}