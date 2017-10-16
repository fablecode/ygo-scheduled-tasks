using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;

namespace ygo_scheduled_tasks.application.unit.tests.ScheduledTasksTests.Handlers
{
    [TestFixture]
    public class CardInformationTaskHandlerTests
    {
        private CardInformationTaskHandler _sut;
        private IArticleCategoryProcessor _articleCategoryProcessor;
        private ISemanticSearchProcessor _semanticSearchProcessor;

        [SetUp]
        public void Setup()
        {
            _articleCategoryProcessor = Substitute.For<IArticleCategoryProcessor>();
            _semanticSearchProcessor = Substitute.For<ISemanticSearchProcessor>();

            _sut = new CardInformationTaskHandler(_articleCategoryProcessor, _semanticSearchProcessor, new CardInformationTaskValidator());
        }

        [Test]
        public async Task Given_An_Invalid_CardInformationTask_Should_Return_Errors()
        {
            // Arrange
            var task = new CardInformationTask();

            // Act
            var result = await _sut.Handle(task);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task Given_An_Invalid_CardInformationTask_Should_Not_Execute_AllCardsTask()
        {
            // Arrange
            var task = new CardInformationTask();
            _articleCategoryProcessor.Process(Arg.Any<string>(), Arg.Any<int>()).Returns(new ArticleBatchTaskResult());

            // Act
            await _sut.Handle(task);

            // Assert
            await _articleCategoryProcessor.DidNotReceive().Process(Arg.Any<string>(), Arg.Any<int>());
        }

        [Test]
        public async Task Given_An_Invalid_CardInformationTask_Should_Not_Execute_SemanticSearch()
        {
            // Arrange
            var task = new CardInformationTask();
            _articleCategoryProcessor.Process(Arg.Any<string>(), Arg.Any<int>()).Returns(new ArticleBatchTaskResult());
            _semanticSearchProcessor.ProcessUrl(Arg.Any<string>(), Arg.Any<string>()).Returns(new SemanticSearchBatchTaskResult());

            // Act
            await _sut.Handle(task);

            // Assert
            await _semanticSearchProcessor.DidNotReceive().ProcessUrl(Arg.Any<string>(), Arg.Any<string>());
        }

    }
}