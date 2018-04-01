using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Decorator;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.domain.integration.tests.ETLTests.ArtistListTests.ProcessorTests.DecoratorTests
{
    [TestFixture]
    public class ArticleProcessLoggerDecoratorTests
    {
        private ArticleProcessLoggerDecorator _sut;
        private IArticleProcessor _articleProcessor;

        [SetUp]
        public void Setup()
        {
            _articleProcessor = Substitute.For<IArticleProcessor>();

            _sut = new ArticleProcessLoggerDecorator(_articleProcessor);
        }

        [Test]
        public async Task Given_A_Category_And_A_SemanticCard_Should_Execute_Process()
        {
            // Arrange
            var category = "Flip";
            var unexpandedArticle = new UnexpandedArticle { Title = "Red-Eyes Black Dragon" };

            _articleProcessor.Process(Arg.Any<string>(), Arg.Any<UnexpandedArticle>()).Returns(new ArticleTaskResult());

            // Act
            await _sut.Process(category, unexpandedArticle);

            // Assert
            await _articleProcessor.Received(1).Process(Arg.Any<string>(), Arg.Any<UnexpandedArticle>());
        }
    }
}