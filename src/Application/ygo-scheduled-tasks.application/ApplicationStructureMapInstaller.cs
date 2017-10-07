using System.Configuration;
using FluentValidation;
using MediatR;
using StructureMap;
using wikia.Api;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.ETL.DataSource;
using ygo_scheduled_tasks.domain.ETL.Processor;
using ygo_scheduled_tasks.domain.ETL.Processor.Decorator;
using ygo_scheduled_tasks.domain.ETL.Processor.Handler;
using ygo_scheduled_tasks.domain.ETL.Processor.Process;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.application
{
    public class ApplicationStructureMapInstaller : Registry
    {
        public ApplicationStructureMapInstaller()
        {
            For<IConfig>().Use((Config)ConfigurationManager.GetSection("ygo-settings"));

            var config = new Container(this).GetInstance<IConfig>();

            Scan
            (
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.AddAllTypesOf(typeof(AbstractValidator<>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IValidator<>));

                    scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>)); // Handlers with no response
                    scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>)); // Handlers with a response
                    scan.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<>)); // Async handlers with no response
                    scan.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>)); // Async Handlers with a response
                    scan.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
                }
            );

            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => ctx.GetInstance);
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => ctx.GetAllInstances);
            For<IMediator>().Use<Mediator>();


            For<IWikiArticle>().Use(context => new WikiArticle(config.WikiaDomainUrl));
            For<IArticleHandler>().Use<ArticleHandler>();
            For<ICardWebPage>().Use<CardWebPage>();
            For<ICardHtmlDocument>().Use<CardHtmlDocument>();
            For<IHtmlWebPage>().Use<HtmlWebPage>();
            For<ICardHtmlTable>().Use<CardHtmlTable>();
            For<ICategoryProcessor>().Use<ArticleCategoryProcessor>();
            For<ICategoryDataSource>().Use<ArticleCategoryDataSource>();
            For<IArticleBatchProcessor>().Use<ArticleBatchProcessor>();

            For<ICommandMapper>().Use<CommandMapper>();
            For<IBatchItemProcessor>().Use<CardItemProcessor>();

            // decorator pattern for logging using structuremap. 
            For<IArticleProcessor>().DecorateAllWith<ArticleProcessLoggerDecorator>();
        }
    }
}