using FluentAssertions;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.Helpers;

namespace ygo_scheduled_tasks.domain.unit.tests
{
    [TestFixture]
    public class ArchetypeHelperTests
    {
        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(" ", null)]
        [TestCase("List of \"Abyss Script\" cards", "Abyss Script")]
        [TestCase("List of \"Magician (manga)\" cards", "Magician (manga)")]
        [TestCase("List of \"F.A.\" cards", "F.A.")]
        [TestCase("List of \"Cyberdark (Spell/Trap archetype)\" cards", "Cyberdark (Spell/Trap archetype)")]
        public void Given_Article_Title_Should_Extract_Archetype_Name(string articleTitle, string expected)
        {
            // Arrange

            // Act
            var result = ArchetypeHelper.ExtractArchetypeName(articleTitle);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("https://vignette.wikia.nocookie.net/yugioh/images/6/65/EvolzarLaggia-TF06-JP-VG.png/revision/latest/window-crop/width/200/x-offset/0/y-offset/0/window-width/545/window-height/544?cb=20110928032728", "https://vignette.wikia.nocookie.net/yugioh/images/6/65/EvolzarLaggia-TF06-JP-VG.png")]
        [TestCase("https://vignette.wikia.nocookie.net/yugioh/images/c/c2/AethertheEmpoweringDragon-YS14-EN-C-1E.png/revision/latest/window-crop/width/200/x-offset/0/y-offset/0/window-width/308/window-height/308?cb=20140711051108", "https://vignette.wikia.nocookie.net/yugioh/images/c/c2/AethertheEmpoweringDragon-YS14-EN-C-1E.png")]
        [TestCase("", "")]
        public void Given_An_Archetype_Thumbnail_Url_Should_Extract_Url_Without_Image_Sizes(string url, string expect)
        {
            // Arrange

            // Act
            var result = ArchetypeHelper.ExtractThumbnailUrl(url);

            // Assert
            result.Should().BeEquivalentTo(expect);
        }
    }
}