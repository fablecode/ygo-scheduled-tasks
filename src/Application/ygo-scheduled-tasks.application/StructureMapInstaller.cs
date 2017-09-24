using System.Configuration;
using FluentValidation;
using MediatR;
using StructureMap;
using wikia.Api;
using ygo_scheduled_tasks.application.Command;
using ygo_scheduled_tasks.application.ETL.DataSource;
using ygo_scheduled_tasks.application.ETL.Processor;
using ygo_scheduled_tasks.application.ETL.Processor.Handler;
using ygo_scheduled_tasks.application.Services;
using ygo_scheduled_tasks.domain.services.WebPage;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.application
{
    public class StructureMapInstaller : Registry
    {
        public StructureMapInstaller()
        {
            For<IConfig>().Use((Config)ConfigurationManager.GetSection("Wikia"));

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

                    scan.AddAllTypesOf<IBatchItemProcessor>();
                }

            );

            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => ctx.GetInstance);
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => ctx.GetAllInstances);
            For<IMediator>().Use<Mediator>();


            For<IWikiArticle>().Use(context => new WikiArticle(config.DomainUrl));
            For<IArticleHandler>().Use<ArticleHandler>();
            For<ICardWebPage>().Use<CardWebPage>();
            For<ICardHtmlDocument>().Use<CardHtmlDocument>();
            For<IHtmlWebPage>().Use<HtmlWebPage>();
            For<ICardHtmlTable>().Use<CardHtmlTable>();
            For<ICategoryProcessor>().Use<ArticleCategoryProcessor>();
            For<ICategoryDataSource>().Use<ArticleCategoryDataSource>();
            For<IArticleBatchProcessor>().Use<ArticleBatchProcessor>();

            For<ICommandMapper>().Use<CommandMapper>();
        }
    }
}