using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Cards.Tips;

namespace ygo_scheduled_tasks.domain.integration.tests.WebPageTests
{
    [TestFixture]
    public class SemanticSearchTests
    {
        private SemanticSearch _sut;
        private IConfig _config;

        [SetUp]
        public void SetUp()
        {
            _config = Substitute.For<IConfig>();
            _sut = new SemanticSearch(_config, new HtmlWebPage());

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
        }

        [TestCase("http://yugioh.wikia.com/wiki/Card_Tips:Tour_Guide_From_the_Underworld")]
        public void Given_A_SemanticSearch_Url_Should_Extract_Cards_From_Url(string url)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(url);
            var semanticUrl = new TipRelatedHtmlDocument(_config).GetUrl(htmlDocument);

            // Act
            var result = _sut.CardsByUrl(semanticUrl);

            // Assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}