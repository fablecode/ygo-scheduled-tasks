using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using StructureMap;
using Topshelf;
using Topshelf.Quartz.StructureMap;
using Topshelf.StructureMap;

namespace ygo_scheduled_tasks.cardinformation
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                var container = new Container(cfg =>
                {
                    cgf.Scan(
                            scan =>
                            {
                                scan.TheCallingAssembly();
                                scan.WithDefaultConventions();
                                scan.AssembliesFromApplicationBaseDirectory();
                                scan.LookForRegistries();
                            });
                });

                x.UseStructureMap(container);

                x.Service<CardInformationService>(s =>
                {
                    s.ConstructUsingStructureMap();

                    s.WhenStarted(service => service.OnStart());
                    s.WhenStopped(service => service.OnStop());
                    s.WhenShutdown(service => service.OnShutdown());

                    s.UseQuartzStructureMap();

                    s.ScheduleQuartzJob(q => 
                        q.WithJob(() => 
                            JobBuilder.Create<CardInformationJob>().Build())
                            .AddTrigger(() => TriggerBuilder.Create()
                                .WithSimpleSchedule(ss => ss
                                    .WithIntervalInHours(168)
                                    .RepeatForever())
                                .Build()));
                });

                x.RunAsLocalSystem()
                    .DependsOnEventLog()
                    .StartAutomatically()
                    .EnableServiceRecovery(rc => rc.RestartService(1));

                x.SetServiceName("Ygo Card Information");
                x.SetDisplayName("Ygo Card Information");
                x.SetDescription("Assemble basic card data for all Yugioh cards.");
            });
        }
    }

    public class CardInformationJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class CardInformationService
    {
        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void OnShutdown()
        {
            throw new NotImplementedException();
        }
    }
}
