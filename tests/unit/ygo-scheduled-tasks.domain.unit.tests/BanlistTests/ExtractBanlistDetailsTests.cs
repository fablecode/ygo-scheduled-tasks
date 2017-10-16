using NUnit.Framework;
using ygo_scheduled_tasks.core.Enums;

namespace ygo_scheduled_tasks.domain.unit.tests.BanlistTests
{
    [TestFixture]
    public class ExtractBanlistDetailsTests
    {
        [TestCase("OCG in effect since September 1, 2007.", BanlistType.Ocg)]
        [TestCase("TCG in effect since January 1, 2016.", BanlistType.Tcg)]
        [TestCase("TCG in effect since April 1, 2010", BanlistType.Tcg)]
        public void Given_A_Banlist_TitleText_Should_Extract_BanlistType(string titleText, BanlistType expected)
        {
            // Arrange

            // Act

            // Assert
        }
    }

}