using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.ScheduledTasks.ArchetypeInformation;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.application.unit.tests.ScheduledTasksTests.Handlers
{
    [TestFixture]
    public class ArchetypeInformationTaskHandlerTests
    {
        private ArchetypeInformationTaskHandler _sut;
        private IArticleCategoryProcessor _articleCategoryProcessor;

        [SetUp]
        public void Setup()
        {
            _articleCategoryProcessor = Substitute.For<IArticleCategoryProcessor>();

            _sut = new ArchetypeInformationTaskHandler(_articleCategoryProcessor, new ArchetypeInformationTaskValidator());
        }

        [Test]
        public async Task Given_An_Invalid_ArchetypeInformationTTask_Should_Return_Errors()
        {
            // Arrange
            var task = new ArchetypeInformationTask();

            // Act
            var result = await _sut.Handle(task);

            // Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task Given_An_Invalid_ArchetypeInformationTask_Should_Not_Execute_Process()
        {
            // Arrange
            var task = new ArchetypeInformationTask();
            _articleCategoryProcessor.Process(Arg.Any<string>(), Arg.Any<int>()).Returns(new ArticleBatchTaskResult());

            // Act
            await _sut.Handle(task);

            // Assert
            await _articleCategoryProcessor.DidNotReceive().Process(Arg.Any<string>(), Arg.Any<int>());
        }
    }
}