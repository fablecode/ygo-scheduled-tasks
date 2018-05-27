using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ygo_scheduled_tasks.core.Enums;
using ygo_scheduled_tasks.domain.ETL.Banlist;
using ygo_scheduled_tasks.domain.ETL.Banlist.DataSource;
using ygo_scheduled_tasks.domain.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Banlists;

namespace ygo_scheduled_tasks.domain.integration.tests.ETLTests.LatestBanlistTests
{
    [TestFixture]
    public class BanlistUrlDataSourceTest
    {
        private BanlistUrlDataSource _sut;

        [SetUp]
        public void Setup()
        {
            var config = Substitute.For<IConfig>();
            _sut = new BanlistUrlDataSource(new BanlistWebPage(new HtmlWebPage(), config, new BanlistHtmlDocument(new HtmlWebPage())), new HtmlWebPage());

            config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
        }

        [TestCase(BanlistType.Tcg, "http://yugioh.wikia.com/wiki/July_1999_Lists")]
        [TestCase(BanlistType.Ocg, "http://yugioh.wikia.com/wiki/January_2018_Lists")]
        public void Given_A_Banlist_Web_Url_And_BanlistType_Should_Return_HtmlNode_List_Of_Banlists_Ids(BanlistType banlistType, string banlistUrl)
        {
            // Arrange

            // Act
            var banlistsIds = _sut.GetBanlists(banlistType, banlistUrl);

            // Assert
            banlistsIds.Should().ContainKey(2005);
        }
    }
}