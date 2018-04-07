using System;
using FluentAssertions;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.Helpers;

namespace ygo_scheduled_tasks.domain.unit.tests.HelperTests
{
    [TestFixture]
    public class StringHelpersTests
    {
        [TestCase("Fishborg Blaster 「フィッシュボーグ－ガンナー」", "Fishborg Blaster")]
        [TestCase("Majespecter Unicorn - Kirin 「マジェスペクター・ユニコーン」", "Majespecter Unicorn - Kirin")]
        [TestCase("The Tyrant Neptune 「Ｔｈｅ　ｔｙｒａｎｔ　ＮＥＰＴＵＮＥザ・タイラント・ネプチューン」", "The Tyrant Neptune")]
        public void Given_A_CardName_With_Japanese_Characters_Should_Remove_Invalid_Characters(string cardName, string expected)
        {
            // Arrange
            const char beginChar = '「';
            const char endChar = '」';

            // Act
            var result = StringHelpers.RemoveBetween(cardName, beginChar, endChar);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("List of \"Fire King\" cards", "Fire King")]
        [TestCase("List of \"Gravekeeper's\" cards", "Gravekeeper's")]
        [TestCase("List of \"/Assault Mode\" cards", "/Assault Mode")]
        public void Given_An_Archetype_ListTitle_Should_Return_Archetype_Name(string archetypeListTitle, string expected)
        {
            // Arrange
            // Act
            var result = StringHelpers.ArchetypeNameFromListTitle(archetypeListTitle);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Given_An_Invalid_Archetype_ListTitle_Should_Throw_ArgumentNullException()
        {
            // Arrange
            // Act
            Action act = () => StringHelpers.ArchetypeNameFromListTitle(null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

    }
}