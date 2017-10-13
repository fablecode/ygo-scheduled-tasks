using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks.Dataflow;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.DataSource;

namespace ygo_scheduled_tasks.domain.unit.tests.ProcessorTests.DataSourceTests
{
    [TestFixture]
    public class SemanticSearchDataSourceTests
    {
        private ISemanticSearch _semanticSearch;
        private SemanticSearchDataSource _sut;

        [SetUp]
        public void Setup()
        {
            _semanticSearch = Substitute.For<ISemanticSearch>();
            _sut = new SemanticSearchDataSource(_semanticSearch);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_A_Invalid_Category_Should_Throw_ArgumentException(string url)
        {
            // Arrange

            // Act
            Action act = () => _sut.Producer(url,  new BufferBlock<SemanticCard[]>());

            // Assert
            act.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Given_A_Invalid_ITargetBlock_Should_Throw_ArgumentException()
        {
            // Arrange

            // Act
            Action act = () => _sut.Producer("url", null);

            // Assert
            act.ShouldThrow<ArgumentException>();
        }
    }
}