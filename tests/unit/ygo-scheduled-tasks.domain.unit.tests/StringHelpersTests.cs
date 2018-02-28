using FluentAssertions;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.Helpers;

namespace ygo_scheduled_tasks.domain.unit.tests
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
            result.ShouldBeEquivalentTo(expected);
        }
    }
}