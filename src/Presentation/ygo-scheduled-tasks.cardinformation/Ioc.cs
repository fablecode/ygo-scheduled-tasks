using StructureMap;
using StructureMap.Graph.Scanning;

namespace ygo_scheduled_tasks.cardinformation
{
    public static class Ioc
    {
        public static Container Initialize()
        {

            var container = new Container(cfg =>
            {
                cfg.Scan
                (
                    scan =>
                    {
                        scan.AssembliesFromApplicationBaseDirectory();
                        scan.WithDefaultConventions();
                        scan.LookForRegistries();
                    }
                );
            });

            // Should throw an exception if any error occurs if loading dlls, which silently fail.
            TypeRepository.AssertNoTypeScanningFailures();

            var result = container.WhatDidIScan();

            return container;
        }
    }

}