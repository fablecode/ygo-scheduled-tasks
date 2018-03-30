using FluentAssertions;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.WebPage.Cards;

namespace ygo_scheduled_tasks.domain.integration.tests.WebPageTests.CardsTests
{
    [TestFixture]
    public class CardHtmlTableTests
    {
        private CardHtmlTable _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CardHtmlTable();
        }

        [Test]
        public void Given_CardHtmlTable_Collection_And_CardLookup_Is_Not_Initialized_Should_Return_Empty_String()
        {
            // Arrange

            // Act
            var result = _sut.GetValue();

            // Assert
            result.Should().BeEmpty();
        }
    }
}