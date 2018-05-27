using FluentAssertions;
using NUnit.Framework;
using ygo_scheduled_tasks.core.Enums;
using ygo_scheduled_tasks.domain.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Banlists;

namespace ygo_scheduled_tasks.domain.integration.tests.ETLTests.LatestBanlistTests
{
    [TestFixture]
    public class BanlistHtmlDocumentTest
    {
        private BanlistHtmlDocument _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new BanlistHtmlDocument(new HtmlWebPage());
        }

        [TestCase(BanlistType.Tcg, "http://yugioh.wikia.com/wiki/July_1999_Lists")]
        [TestCase(BanlistType.Ocg, "http://yugioh.wikia.com/wiki/January_2018_Lists")]
        public void Given_A_Banlist_Web_Url_And_BanlistType_Should_Return_HtmlNode_List_Of_Available_Banlists(BanlistType banlistType, string banlistUrl)
        {
            // Arrange

            // Act
            var htmlNodeList = _sut.GetBanlistHtmlNode(banlistType, banlistUrl);

            // Assert
            htmlNodeList.Name.Should().BeEquivalentTo("ul");
        }

    }
}