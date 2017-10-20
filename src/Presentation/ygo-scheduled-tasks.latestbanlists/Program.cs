using System;
using Quartz;
using Topshelf;
using Topshelf.Quartz.StructureMap;
using Topshelf.StructureMap;

namespace ygo_scheduled_tasks.latestbanlists
{
    class Program
    {
        private const string CronExpression = "0 58 23 ? * SUN *"; // Every Sunday, 23:58pm

        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                var container = Ioc.Initialize();
                x.UseStructureMap(container);
                x.UseNLog();

                x.Service<BanlistInformationService>(s =>
                {
                    s.ConstructUsingStructureMap();

                    s.BeforeStartingService(_ => Console.WriteLine("Before Start"));
                    s.BeforeStoppingService(_ => Console.WriteLine("Before Stop"));

                    s.WhenStarted(service => service.OnStart());
                    s.WhenStopped(service => service.OnStop());

                    s.UseQuartzStructureMap();

                    s.ScheduleQuartzJob(q =>
                        q.WithJob(() =>
                                JobBuilder.Create<BanlistInformationJob>().Build())
                            .AddTrigger(() => TriggerBuilder.Create()
                                //.WithCronSchedule(CronExpression)
                                .StartNow()
                                .Build()));
                });

                x.RunAsLocalSystem()
                    .DependsOnEventLog()
                    .StartAutomatically()
                    .EnableServiceRecovery(rc => rc.RestartService(1));

                var ygoCardInformation = "YgoBanlistInformation";

                x.SetServiceName(ygoCardInformation);
                x.SetDisplayName(ygoCardInformation);
                x.SetDescription("Amalgamate banlist data.");
            });
        }
    }
}
