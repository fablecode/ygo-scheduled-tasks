﻿using System.Configuration;
using Common.Logging;
using MediatR;
using StructureMap;
using ygo_scheduled_tasks.application;

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
                        scan.TheCallingAssembly();
                        scan.WithDefaultConventions();
                        scan.AssembliesFromApplicationBaseDirectory();
                        scan.LookForRegistries();
                    }
                );
            });

            return container;
        }
    }

}