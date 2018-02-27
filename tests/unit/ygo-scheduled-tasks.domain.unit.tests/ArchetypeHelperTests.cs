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
            result.ShouldBeEquivalentTo(expected);
        }
    }
}