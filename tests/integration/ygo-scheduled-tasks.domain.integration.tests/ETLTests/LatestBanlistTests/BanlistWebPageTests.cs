using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ygo_scheduled_tasks.core.Enums;
using ygo_scheduled_tasks.domain.ETL.Banlist;
using ygo_scheduled_tasks.domain.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Banlists;

namespace ygo_scheduled_tasks.domain.integration.tests.ETLTests.LatestBanlistTests
{
    public class BanlistWebPageTests
    {
        private BanlistWebPage _sut;

        [SetUp]
        public void SetUp()
        {
            var config = Substitute.For<IConfig>();

            _sut = new BanlistWebPage(new HtmlWebPage(), config, new BanlistHtmlDocument(new HtmlWebPage()));

            config.ApiUrl.Returns("http://yugioh.wikia.com");
        }

        [TestCase(BanlistType.Tcg, "http://yugioh.wikia.com/wiki/July_1999_Lists")]
        public void Given_A_Banlist_Web_Url_And_BanlistType_Should_Contain_Key(BanlistType banlistType, string banlistUrl)
        {
            // Arrange

            // Act
            var banlistUrls = _sut.GetBanlistUrlList(banlistType, banlistUrl);

            // Assert
            banlistUrls.Should().ContainKey("2005");
        }

        [TestCase(BanlistType.Tcg, "http://yugioh.wikia.com/wiki/July_1999_Lists")]
        public void Given_A_Banlist_Web_Url_And_BanlistType_Should_Have_A_Count_Greater_Than_1(BanlistType banlistType, string banlistUrl)
        {
            // Arrange

            // Act
            var banlistUrls = _sut.GetBanlistUrlList(banlistType, banlistUrl);

            // Assert
            banlistUrls.Should().HaveCountGreaterThan(1);
        }

    }
}