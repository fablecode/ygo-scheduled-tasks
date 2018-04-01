using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Decorator;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;

namespace ygo_scheduled_tasks.domain.integration.tests.ETLTests.SemanticSearchTests.ProcessorTests.DecoratorTest
{
    [TestFixture]
    public class SemanticCardProcessorLoggerDecoratorTests
    {
        private SemanticCardProcessorLoggerDecorator _sut;
        private ISemanticCardProcessor _semanticCardProcessor;

        [SetUp]
        public void Setup()
        {
            _semanticCardProcessor = Substitute.For<ISemanticCardProcessor>();

            _sut = new SemanticCardProcessorLoggerDecorator(_semanticCardProcessor);
        }

        [Test]
        public async Task Given_A_Category_And_A_SemanticCard_Should_Execute_Process()
        {
            // Arrange
            var category = "Flip";
            var semanticCard = new SemanticCard {Name = "Red-Eyes Black Dragon"};
            _semanticCardProcessor.Process(Arg.Any<string>(), Arg.Any<SemanticCard>()).Returns(new SemanticSearchTaskResult());

            // Act
            await _sut.Process(category, semanticCard);

            // Assert
            await _semanticCardProcessor.Received(1).Process(Arg.Any<string>(), Arg.Any<SemanticCard>());
        }
    }
}