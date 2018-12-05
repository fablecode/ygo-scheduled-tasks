using FluentAssertions;
using HtmlAgilityPack;
using NSubstitute;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.WebPage.Cards.Tips;

namespace ygo_scheduled_tasks.domain.integration.tests.WebPageTests
{
    [TestFixture]
    public class TipRelatedHtmlDocumentTests
    {
        private TipRelatedHtmlDocument _sut;

        [SetUp]
        public void SetUp()
        {
            var config = Substitute.For<IConfig>();

            _sut = new TipRelatedHtmlDocument(config);

            config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");

        }

        [TestCase("http://yugioh.wikia.com/wiki/Card_Tips:Sangan")]
        public void Given_A_Tip_Url_Should_Return_Tip_Card_Table(string url)
        {
            // Arrange
            var htmlDocument = new HtmlWeb().Load(url);

            // Act
            var result = _sut.GetTable(htmlDocument);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().BeEquivalentTo("table");
        }

        [TestCase("http://yugioh.wikia.com/wiki/Card_Tips:Sangan", "http://yugioh.wikia.com/wiki/Special:Ask/-5B-5BClass-201::Official-5D-5D-20-5B-5BConcept:Main-20Deck-20monsters-5D-5D-20-5B-5BATK::%E2%89%A41500-5D-5D/-3FJapanese-20name/-3FPrimary-20type/-3FSecondary-20type/-3FAttribute/-3FType/-3FLevel/-3FPendulum-20Scale%3DPS/-3FATK-20string%3DATK/-3FDEF-20string%3DDEF/mainlabel%3D/limit%3D10/offset%3D10/format%3Dtable")]
        public void Given_A_Tip_Url_Should_Return_FurtherResults_Url(string url, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWeb().Load(url);

            // Act
            var result = _sut.GetUrl(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

    }
}