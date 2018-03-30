using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using wikia.Api;
using ygo_scheduled_tasks.domain.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Archetypes;

namespace ygo_scheduled_tasks.domain.integration.tests.WebPageTests
{
    [TestFixture]
    public class ArchetypeThumbnailTests
    {
        private ArchetypeThumbnail _sut;
        private IConfig _config;
        private const string DomainUrl = "http://yugioh.wikia.com";

        [SetUp]
        public void Setup()
        {
            _config = Substitute.For<IConfig>();

            _sut = new ArchetypeThumbnail(new WikiArticle(DomainUrl), new HtmlWebPage(), _config);
        }

        [TestCase("/wiki/Elemental_HERO", "https://vignette.wikia.nocookie.net/yugioh/images/d/d4/FifthHope-TF04-JP-VG.png")]
        [TestCase("/wiki/Archetype", null)]
        public void Given_A_Relative_Url_Should_Return_Thumbnail_Url(string url, string expected)
        {
            // Arrange
            _config.WikiaDomainUrl.Returns(DomainUrl);

            // Act
            var result = _sut.FromWebPage(url);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase(3544, "https://vignette.wikia.nocookie.net/yugioh/images/d/d4/FifthHope-TF04-JP-VG.png")]
        [TestCase(34587, null)]
        public async Task Given_An_ArchetypeNumber_And_Url_Should_Return_Thumbnail_Url(int archetypeNumber, string expected)
        {
            // Arrange
            _config.WikiaDomainUrl.Returns(DomainUrl);

            // Act
            var result = await _sut.FromArticleId(archetypeNumber);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

    }
}