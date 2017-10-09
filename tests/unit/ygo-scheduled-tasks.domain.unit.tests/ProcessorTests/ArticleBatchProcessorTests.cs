﻿using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL;
using ygo_scheduled_tasks.domain.ETL.Processor;

namespace ygo_scheduled_tasks.domain.unit.tests.ProcessorTests
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
            const string expected = "category";

            // Act
            Func<Task<ArticleBatchTaskResult>> act = () =>_sut.Process(category, Arg.Any<UnexpandedArticle[]>());

            // Assert
            act
                .ShouldThrow<ArgumentException>()
                .WithMessage(expected);
        }

        [Test]
        public void Given_A_Null_ArticleList_Collection_Should_Throw_ArgumenException()
        {
            // Arrange
            const string expected = "articles";

            // Act
            Func<Task<ArticleBatchTaskResult>> act = () => _sut.Process("a category", null);

            // Assert
            act
                .ShouldThrow<ArgumentException>()
                .WithMessage(expected);
        }

        [Test]
        public async Task Given_A_Valid_ArticleList_Collection_Should_Increment_Processed_Variable_If_Article_Is_Processed_Successfully()
        {
            // Arrange
            const int expected = 2;

            var fixture = new Fixture { RepeatCount = 3 };
            var articles = fixture.Create<UnexpandedArticle[]>();
            _articleProcessor
                .Process("some category", Arg.Any<UnexpandedArticle>())
                .ReturnsForAnyArgs
                (
                    x => new ArticleTaskResult { IsSuccessfullyProcessed = true },
                    x => new ArticleTaskResult { IsSuccessfullyProcessed = true},
                    x => new ArticleTaskResult()
                );

            // Act
            var result = await _sut.Process("a category", articles);

            // Assert
            result.Processed.Should().Be(expected);
        }

        [Test]
        public async Task Given_A_Valid_ArticleList_Collection_Should_Log_Unsuccessfully_Processed_Articles_In_Failed_Collection()
        {
            // Arrange
            const int expected = 2;

            var fixture = new Fixture { RepeatCount = 4 };
            var articles = fixture.Create<UnexpandedArticle[]>();
            _articleProcessor
                .Process("some category", Arg.Any<UnexpandedArticle>())
                .ReturnsForAnyArgs
                (
                    x => new ArticleTaskResult { IsSuccessfullyProcessed = true },
                    x => new ArticleTaskResult { IsSuccessfullyProcessed = true },
                    x => null,
                    x => null
                );

            // Act
            var result = await _sut.Process("a category", articles);

            // Assert
            result.Failed.Count.Should().Be(expected);
        }

        [Test]
        public async Task Given_A_Valid_ArticleList_Collection_Should_Execute_Process()
        {
            // Arrange
            var fixture = new Fixture { RepeatCount = 4 };
            var articles = fixture.Create<UnexpandedArticle[]>();
            _articleProcessor
                .Process("some category", Arg.Any<UnexpandedArticle>())
                .ReturnsForAnyArgs
                (
                    x => new ArticleTaskResult { IsSuccessfullyProcessed = true },
                    x => new ArticleTaskResult { IsSuccessfullyProcessed = true },
                    x => null,
                    x => null
                );

            // Act
            await _sut.Process("a category", articles);

            // Assert
            await _articleProcessor.Received(4).Process(Arg.Any<string>(), Arg.Any<UnexpandedArticle>());
        }

    }
}