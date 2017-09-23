using System;
using System.Collections.Generic;
using MediatR;
using Quartz;
using Topshelf;
using Topshelf.Quartz.StructureMap;
using Topshelf.StructureMap;
using ygo_scheduled_tasks.application;
using ygo_scheduled_tasks.application.ETL;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

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

                    s.BeforeStartingService(_ => Console.WriteLine("BeforeStart"));
                    s.BeforeStoppingService(_ => Console.WriteLine("BeforeStop"));

                    s.WhenStarted(service => service.OnStart());
                    s.WhenStopped(service => service.OnStop());

                    s.UseQuartzStructureMap();

                    s.ScheduleQuartzJob(q => 
                        q.WithJob(() => 
                            JobBuilder.Create<CardInformationJob>().Build())
                            .AddTrigger(() => TriggerBuilder.Create()
                                .WithCalendarIntervalSchedule(ss =>
                                    ss.WithIntervalInWeeks(1)
                                    .Build())
                                .StartNow()
                                .Build()));
                });

                x.RunAsLocalSystem()
                    .DependsOnEventLog()
                    .StartAutomatically()
                    .EnableServiceRecovery(rc => rc.RestartService(1));

                x.SetServiceName("Yugioh Card Information");
                x.SetDisplayName("Yugioh Card Information");
                x.SetDescription("Amalgamate card insight data, for all Yugioh cards.");
            });
        }
    }

    public class CardInformationJob : IJob
    {
        private readonly IMediator _mediator;

        public CardInformationJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Execute(IJobExecutionContext context)
        {
            const int pageSize = 200;
            var categories = new List<string> { ArticleCategory.TcgCards, ArticleCategory.OcgCards, ArticleCategory.MonsterCardTypes};

            _mediator.Send(new CardInformationTask { Categories = categories, PageSize = pageSize }).Wait();
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
    }
}
