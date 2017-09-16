using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.unit.tests.ScheduledTasksTests.Handlers
{
    [TestFixture]
    public class CardInformationTaskHandlerTests
    {
        private CardInformationTaskHandler _sut;
        private IMediator _mediator;

        [SetUp]
        public void Setup()
        {
            _mediator = Substitute.For<IMediator>();

            _sut = new CardInformationTaskHandler(_mediator, new CardInformationTaskValidator());
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
            _mediator.Send(task).Returns(new CardInformationTaskResult());

            // Act
            await _sut.Handle(task);

            // Assert
            await _mediator.DidNotReceive().Send(task);
        }

    }
}