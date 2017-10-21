using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.ScheduledTasks.LatestBanlist;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.application.unit.tests.ScheduledTasksTests.Handlers
{
    [TestFixture]
    public class BanlistInformationTaskHandlerTests
    {
        private BanlistInformationTaskHandler _sut;
        private IArticleCategoryProcessor _articleCategoryProcessor;

        [SetUp]
        public void Setup()
        {
            _articleCategoryProcessor = Substitute.For<IArticleCategoryProcessor>();

            _sut = new BanlistInformationTaskHandler(_articleCategoryProcessor, new BanlistInformationTaskValidator());
        }

        [Test]
        public async Task Given_An_Invalid_BanlistInformationTask_Should_Return_Errors()
        {
            // Arrange
            var task = new BanlistInformationTask();

            // Act
            var result = await _sut.Handle(task);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task Given_An_Invalid_BanlistInformationTask_Should_Not_Execute_BanlistTask()
        {
            // Arrange
            var task = new BanlistInformationTask();
            _articleCategoryProcessor.Process(Arg.Any<string>(), Arg.Any<int>()).Returns(new ArticleBatchTaskResult());

            // Act
            await _sut.Handle(task);

            // Assert
            await _articleCategoryProcessor.DidNotReceive().Process(Arg.Any<string>(), Arg.Any<int>());
        }
    }
}