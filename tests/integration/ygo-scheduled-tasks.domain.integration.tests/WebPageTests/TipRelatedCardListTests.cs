using FluentAssertions;
using HtmlAgilityPack;
using NSubstitute;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.WebPage.Cards.Tips;

namespace ygo_scheduled_tasks.domain.integration.tests.WebPageTests
{
    [TestFixture]
    public class TipRelatedCardListTests
    {
        private TipRelatedCardList _sut;
        private IConfig _config;

        [SetUp]
        public void Setup()
        {
            _config = Substitute.For<IConfig>();
            _sut = new TipRelatedCardList(_config);

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
        }
        [TestCase("http://yugioh.wikia.com/wiki/Card_Tips:Sangan", 10)]
        public void Given_A_Card_Tip_Url_Should_Extract_RelatedCards_From_Table(string url, int expected)
        {
            // Arrange
            var htmlDocument = new HtmlWeb().Load(url);
            var htmlTable = new TipRelatedHtmlDocument(_config).GetTable(htmlDocument);

            // Act
            var result = _sut.ExtractCardsFromTable(htmlTable);

            // Assert
            result.Should().HaveCount(expected);
        }
    }
}