using FluentValidation;
using MediatR;
using StructureMap;
using System.Configuration;
using wikia.Api;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.ETL.ArticleList.DataSource;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Decorator;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Handler;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Decorator;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Handler;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Process;
using ygo_scheduled_tasks.domain.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Archetypes;
using ygo_scheduled_tasks.domain.WebPage.Cards;

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
                    scan.ConnectImplementationsToTypesClosing(typeof(ICancellableAsyncNotificationHandler<>));
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
            For<IArchetypeWebPage>().Use<ArchetypeWebPage>();
            For<ICardHtmlDocument>().Use<CardHtmlDocument>();
            For<IHtmlWebPage>().Use<HtmlWebPage>();
            For<ICardHtmlTable>().Use<CardHtmlTable>();
            For<IArticleCategoryProcessor>().Use<ArticleCategoryProcessor>();
            For<IArticleCategoryDataSource>().Use<ArticleCategoryDataSource>();
            For<IArticleBatchProcessor>().Use<ArticleBatchProcessor>();

            For<ICommandMapper>().Use<CommandMapper>();
            For<IArticleItemProcessor>().Use<CardItemProcessor>();
            For<IArticleItemProcessor>().Use<BanlistItemProcessor>();
            For<IArticleItemProcessor>().Use<CardsByArchetypeItemProcessor>();


            For<ISemanticCardHandler>().Use<SemanticCardHandler>();
            For<ISemanticSearch>().Use<SemanticSearch>();
            For<ISemanticCardProcessor>().Use<SemanticCardProcessor>();
            For<ISemanticSearchBatchProcessor>().Use<SemanticSearchBatchProcessor>();

            For<ISemanticCardItemProcess>().Use<SemanticSearchNormalMonstersProcessor>();
            For<ISemanticCardItemProcess>().Use<SemanticSearchFlipMonstersProcessor>();

            // decorator pattern for logging using structuremap. 
            For<IArticleProcessor>().DecorateAllWith<ArticleProcessLoggerDecorator>();
            For<ISemanticCardProcessor>().DecorateAllWith<SemanticCardProcessorLoggerDecorator>();


        }
    }
}