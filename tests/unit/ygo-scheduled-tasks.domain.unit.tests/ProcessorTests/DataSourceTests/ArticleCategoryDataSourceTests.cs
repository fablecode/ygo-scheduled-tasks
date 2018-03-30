using AutoFixture;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks.Dataflow;
using wikia.Api;
using wikia.Models.Article;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL.ArticleList.DataSource;

namespace ygo_scheduled_tasks.domain.unit.tests.ProcessorTests.DataSourceTests
{
    [TestFixture]
    public class ArticleCategoryDataSourceTests
    {
        private IWikiArticle _wikiArticle;
        private ArticleCategoryDataSource _sut;

        [SetUp]
        public void Setup()
        {
            _wikiArticle = Substitute.For<IWikiArticle>();
            _sut = new ArticleCategoryDataSource(_wikiArticle);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_A_Invalid_Category_Should_Throw_ArgumentException(string category)
        {
            // Arrange

            // Act
            Action act = () => _sut.Producer(category, 500, new BufferBlock<UnexpandedArticle[]>()).Wait();

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Given_A_Invalid_ITargetBlock_Should_Throw_ArgumentException()
        {
            // Arrange

            // Act
            Action act = () => _sut.Producer("category", 500, null).Wait();

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Given_Batches_Of_UnexpandedArticle_Should_Process_All_Batches()
        {
            // Arrange
            var expected = 5;
            var pageSize = 100;
            var articleBatchBufferBlock = new BufferBlock<UnexpandedArticle[]>();

            var fixture = new Fixture();

            var articleBatch1 = fixture.Build<UnexpandedListArticleResultSet>().With(x => x.Items, new Fixture { RepeatCount = pageSize }.Create<UnexpandedArticle[]>()).Create();
            var articleBatch2 = fixture.Build<UnexpandedListArticleResultSet>().With(x => x.Items, new Fixture { RepeatCount = pageSize }.Create<UnexpandedArticle[]>()).Create();
            var articleBatch3 = fixture.Build<UnexpandedListArticleResultSet>().With(x => x.Items, new Fixture { RepeatCount = pageSize }.Create<UnexpandedArticle[]>()).Create();
            var articleBatch4 = fixture.Build<UnexpandedListArticleResultSet>().With(x => x.Items, new Fixture { RepeatCount = pageSize }.Create<UnexpandedArticle[]>()).Create();
            var articleBatch5 = fixture.Build<UnexpandedListArticleResultSet>().With(x => x.Items, new Fixture { RepeatCount = pageSize }.Create<UnexpandedArticle[]>()).Create();

            // Set Last page
            articleBatch5.Offset = null;

            _wikiArticle
                .AlphabeticalList(Arg.Any<ArticleListRequestParameters>())
                .ReturnsForAnyArgs
                (
                    articleBatch1,
                    articleBatch2,
                    articleBatch3,
                    articleBatch4,
                    articleBatch5
                );

            // Act
            _sut.Producer("a category", 500, articleBatchBufferBlock).Wait();


            // Assert
            articleBatchBufferBlock.Count.Should().Be(expected);
        }


    }
}