using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.application.ETL.Processor;
using ygo_scheduled_tasks.application.ScheduledTasks.CardInformation;

namespace ygo_scheduled_tasks.application.unit.tests.ProcessorTests
{
    [TestFixture]
    public class ArticleBatchProcessorTests
    {
        private ArticleBatchProcessor _sut;
        private IArticleProcessor _articleProcessor;

        [SetUp]
        public void Setup()
        {
            _articleProcessor = Substitute.For<IArticleProcessor>();
            _sut = new ArticleBatchProcessor(_articleProcessor);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_Category_Should_Throw_ArgumentException(string category)
        {
            // Arrange

            // Act
            Func<Task<ArticleBatchTaskResult>> act = () =>_sut.Process(category, Arg.Any<UnexpandedArticle[]>());

            // Assert
            act.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Given_A_Null_ArticleList_Should_Throw_ArgumenException()
        {
            // Arrange

            // Act
            Func<Task<ArticleBatchTaskResult>> act = () => _sut.Process("some category", null);

            // Assert
            act.ShouldThrow<ArgumentException>();
        }

        [Test]
        public async Task Given_UnexpandedArticles_Should_Increment_Processed_Variable()
        {
            // Arrange
            var expected = 2;

            var fixture = new Fixture { RepeatCount = 3 };
            var articles = fixture.Create<UnexpandedArticle[]>();
            _articleProcessor
                .Process("some category", Arg.Any<UnexpandedArticle>())
                .ReturnsForAnyArgs
                (
                    x => new ArticleTaskResult { Processed = true },
                    x => new ArticleTaskResult { Processed = true},
                    x => new ArticleTaskResult()
                );

            // Act
            var result = await _sut.Process("a category", articles);

            // Assert
            result.Processed.Should().Be(expected);
        }

        [Test]
        public async Task Given_UnexpandedArticles_Should_Log_Failed_UnexpandedArticle_In_Failed_Collection()
        {
            // Arrange
            var expected = 1;

            var fixture = new Fixture { RepeatCount = 3 };
            var articles = fixture.Create<UnexpandedArticle[]>();
            _articleProcessor
                .Process("some category", Arg.Any<UnexpandedArticle>())
                .ReturnsForAnyArgs
                (
                    x => new ArticleTaskResult { Processed = true },
                    x => new ArticleTaskResult { Processed = true },
                    x => null
                );

            // Act
            var result = await _sut.Process("a category", articles);

            // Assert
            result.Failed.Count.Should().Be(expected);
        }

    }
}