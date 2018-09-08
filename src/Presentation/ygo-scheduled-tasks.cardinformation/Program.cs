using System;
using System.Configuration;
using Quartz;
using Topshelf;
using Topshelf.Quartz.StructureMap;
using Topshelf.StructureMap;

namespace ygo_scheduled_tasks.cardinformation
{
    class Program
    {
        private static readonly string CronExpression = ConfigurationManager.AppSettings["CronExpression"]; // Every Day at 4:00am

        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                var container = Ioc.Initialize();
                x.UseStructureMap(container);
                x.UseNLog();

                x.Service<CardInformationService>(s =>
                {
                    s.ConstructUsingStructureMap();

                    s.BeforeStartingService(_ => Console.WriteLine("Before Start"));
                    s.BeforeStoppingService(_ => Console.WriteLine("Before Stop"));

                    s.WhenStarted(service => service.OnStart());
                    s.WhenStopped(service => service.OnStop());

                    s.UseQuartzStructureMap();
                    
                    s.ScheduleQuartzJob(q => 
                        q.WithJob(() => 
                            JobBuilder.Create<CardInformationJob>().Build())
                            .AddTrigger(() => TriggerBuilder.Create()
                            .WithCronSchedule(CronExpression)
                            .StartNow()
                            .Build()));
                });

                x.RunAsLocalSystem()
                    .DependsOnEventLog()
                    .StartAutomatically()
                    .EnableServiceRecovery(rc => rc.RestartService(1));

                var ygoCardInformation = "YgoCardInformation";

                x.SetServiceName(ygoCardInformation);
                x.SetDisplayName(ygoCardInformation);
                x.SetDescription("Amalgamate card insight data, for all Yugioh cards.");
            });
        }
    }
}
