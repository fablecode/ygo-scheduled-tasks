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
    public class ArchetypeWebPageTests
    {
        private ArchetypeWebPage _sut;

        [SetUp]
        public void Setup()
        {
            var config = Substitute.For<IConfig>();
            _sut = new ArchetypeWebPage(config, new HtmlWebPage(), new WikiArticle("http://yugioh.wikia.com"));
        }

        [TestCase(3544, "/wiki/Elemental_HERO", "https://vignette.wikia.nocookie.net/yugioh/images/d/d4/FifthHope-TF04-JP-VG.png")]
        public async Task Given_An_ArchetypeNumber_And_Url_Should_Return_Thumbnail_Url(int archetypeNumber, string url, string expected)
        {
            // Arrange

            // Act
            var result = await _sut.ArchetypeThumbnail(archetypeNumber, url);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}