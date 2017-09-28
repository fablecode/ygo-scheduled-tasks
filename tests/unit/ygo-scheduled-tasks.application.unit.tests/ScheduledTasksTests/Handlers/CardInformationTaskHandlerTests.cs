using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ygo_scheduled_tasks.application.ETL;
using ygo_scheduled_tasks.application.ETL.Processor;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.unit.tests.ScheduledTasksTests.Handlers
{
    [TestFixture]
    public class CardInformationTaskHandlerTests
    {
        private CardInformationTaskHandler _sut;
        private ICategoryProcessor _categoryProcessor;

        [SetUp]
        public void Setup()
        {
            _categoryProcessor = Substitute.For<ICategoryProcessor>();

            _sut = new CardInformationTaskHandler(_categoryProcessor, new CardInformationTaskValidator());
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
            _categoryProcessor.Process(Arg.Any<string>(), Arg.Any<int>()).Returns(new ArticleBatchTaskResult());

            // Act
            await _sut.Handle(task);

            // Assert
            await _categoryProcessor.DidNotReceive().Process(Arg.Any<string>(), Arg.Any<int>());
        }

    }
}