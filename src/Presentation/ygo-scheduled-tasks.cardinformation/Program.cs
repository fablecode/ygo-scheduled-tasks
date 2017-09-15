using System;
using System.Collections.Generic;
using MediatR;
using Quartz;
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
                var container = Ioc.Initialize();

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
                                    .WithIntervalInHours(168 /*Every week*/)
                                    .RepeatForever())
                                .Build()));
                });

                x.RunAsLocalSystem()
                    .DependsOnEventLog()
                    .StartAutomatically()
                    .EnableServiceRecovery(rc => rc.RestartService(1));

                x.SetServiceName("Yugioh Card Information");
                x.SetDisplayName("Yugioh Card Information");
                x.SetDescription("Amalgamate basic card profile data for all Yugioh cards.");
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
        }
    }

    public class CardInformationScheduleTask : IRequest<ScheduleTaskResult>
    {
    }

    public class ScheduleTaskResult
    {
        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }

        public ScheduleTaskStatus ScheduleTaskStatus { get; set; }
    }

    public class ScheduleTaskStatus
    {
        public string Name { get; set; }
    }
}
