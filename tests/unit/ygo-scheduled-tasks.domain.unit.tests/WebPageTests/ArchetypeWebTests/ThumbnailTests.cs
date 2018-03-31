using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Archetypes;

namespace ygo_scheduled_tasks.domain.unit.tests.WebPageTests.ArchetypeWebTests
{
    [TestFixture]
    public class ThumbnailTests
    {
        private ArchetypeWebPage _sut;
        private IConfig _config;
        private IArchetypeThumbnail _archetypeThumbnail;

        [SetUp]
        public void Setup()
        {
            _config = Substitute.For<IConfig>();
            _archetypeThumbnail = Substitute.For<IArchetypeThumbnail>();

            _sut = new ArchetypeWebPage(_config, new HtmlWebPage(), _archetypeThumbnail);
        }

        [Test]
        public async Task Given_An_ArticleId_With_A_Thumbnail_Should_Not_Execute_FromWebPage()
        {
            // Arrange
            const int articleId = 3242;

            _archetypeThumbnail.FromArticleId(articleId).Returns("https://vignette.wikia.nocookie.net/yugioh/images/6/65/EvolzarLaggia-TF06-JP-VG.png/revision/latest/window-crop/width/200/x-offset/0/y-offset/0/window-width/545/window-height/544?cb=20110928032728");

            // Act
            await _sut.ArchetypeThumbnail(articleId, Arg.Any<string>());

            // Assert
            _archetypeThumbnail.DidNotReceive().FromWebPage(Arg.Any<string>());
        }

        [Test]
        public async Task Given_An_ArticleId_Without_A_Thumbnail_Should_Execute_FromWebPage()
        {
            // Arrange
            const int articleId = 3242;

            _archetypeThumbnail.FromArticleId(articleId).Returns((string)null);
            _archetypeThumbnail.FromWebPage(Arg.Any<string>()).Returns("https://vignette.wikia.nocookie.net/yugioh/images/6/65/EvolzarLaggia-TF06-JP-VG.png/revision/latest/window-crop/width/200/x-offset/0/y-offset/0/window-width/545/window-height/544?cb=20110928032728");

            // Act
            await _sut.ArchetypeThumbnail(articleId, Arg.Any<string>());

            // Assert
            _archetypeThumbnail.Received(1).FromWebPage(Arg.Any<string>());
        }

        [Test]
        public async Task Given_An_ArticleId_If_No_Thumbnail_Is_Found_Should_Return_Null()
        {
            // Arrange
            const int articleId = 3242;

            _archetypeThumbnail.FromArticleId(articleId).Returns((string)null);
            _archetypeThumbnail.FromWebPage(Arg.Any<string>()).Returns((string) null);

            // Act
            var result = await _sut.ArchetypeThumbnail(articleId, Arg.Any<string>());

            // Assert
            result.Should().BeNull();
        }

    }
}