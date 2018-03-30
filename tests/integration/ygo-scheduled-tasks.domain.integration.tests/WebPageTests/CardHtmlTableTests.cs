using FluentAssertions;
using NUnit.Framework;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Cards;

namespace ygo_scheduled_tasks.domain.integration.tests.WebPageTests
{
    [TestFixture]
    public class CardHtmlTableTests
    {
        private ICardHtmlTable _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CardHtmlTable();
        }

        [TestCase("http://yugioh.wikia.com/wiki/Monster_Reborn", "Monster Reborn")]
        [TestCase("http://yugioh.wikia.com/wiki/Nobledragon_Magician", "Nobledragon Magician")]
        [TestCase("http://yugioh.wikia.com/wiki/Call_of_the_Haunted", "Call of the Haunted")]
        [TestCase("http://yugioh.wikia.com/wiki/ZW_-_Unicorn_Spear", "ZW - Unicorn Spear")]
        public void Given_A_Valid_Card_Profile_Url_Should_Extract_Card_Name(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetValue(CardHtmlTable.Name);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Given_An_Invalid_Card_Profile_Url_Should_Return_Null()
        {
            // Arrange
            var cardProfileUrl = "http://www.google.co.uk";

            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetValue(CardHtmlTable.Name);

            // Assert
            result.Should().BeNull();
        }


        [TestCase("http://yugioh.wikia.com/wiki/Monster_Reborn", "83764718")]
        [TestCase("http://yugioh.wikia.com/wiki/Nobledragon_Magician", "88935103")]
        [TestCase("http://yugioh.wikia.com/wiki/Call_of_the_Haunted", "97077563")]
        public void Given_A_Valid_Card_Profile_Url_Should_Extract_Card_Number(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetValue(CardHtmlTable.Number);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Nobledragon_Magician", "Monster")]
        [TestCase("http://yugioh.wikia.com/wiki/Monster_Reborn", "Spell")]
        [TestCase("http://yugioh.wikia.com/wiki/Call_of_the_Haunted", "Trap")]
        public void Given_A_Valid_Card_Profile_Url_Should_Extract_Card_CardType(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetValue(CardHtmlTable.CardType);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/A_Legendary_Ocean", "Field")]
        [TestCase("http://yugioh.wikia.com/wiki/Enemy_Controller", "Quick-Play")]
        [TestCase("http://yugioh.wikia.com/wiki/Premature_Burial", "Equip")]
        [TestCase("http://yugioh.wikia.com/wiki/Black_Illusion_Ritual", "Ritual")]
        [TestCase("http://yugioh.wikia.com/wiki/Messenger_of_Peace", "Continuous")]
        [TestCase("http://yugioh.wikia.com/wiki/Monster_Reborn", "Normal")]
        [TestCase("http://yugioh.wikia.com/wiki/Divine_Wrath", "Counter")]
        [TestCase("http://yugioh.wikia.com/wiki/Call_of_the_Haunted", "Continuous")]
        [TestCase("http://yugioh.wikia.com/wiki/Mirror_Force", "Normal")]
        public void Given_A_Valid_Spell_Or_Trap_Card_Profile_Url_Should_Extract_Card_Property(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetValue(CardHtmlTable.Property);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Nobledragon_Magician", "Fire")]
        [TestCase("http://yugioh.wikia.com/wiki/Summoner_Monk", "Dark")]
        public void Given_A_Valid_Monster_Card_Profile_Url_Should_Extract_Card_Attribute(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetCardAttribute();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Given_A_Invalid_Monster_Card_Profile_Url_Should_Extract_Card_Attribute()
        {
            // Arrange
            var cardProfileUrl = "http://www.google.co.uk";
            var expected = string.Empty;

            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetCardAttribute();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }


        [TestCase("http://yugioh.wikia.com/wiki/Summoner_Monk", 4)]
        [TestCase("http://yugioh.wikia.com/wiki/Nobledragon_Magician", 3)]
        public void Given_A_Valid_Monster_Card_Profile_Url_Should_Extract_Card_Level(string cardProfileUrl, int expected)
        {
            // Arrange
            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetIntValue(CardHtmlTable.Level);

            // Assert
            result.Should().Be(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Superdreadnought_Rail_Cannon_Gustav_Max", 10)]
        [TestCase("http://yugioh.wikia.com/wiki/D/D/D_Wave_High_King_Caesar", 6)]
        [TestCase("http://yugioh.wikia.com/wiki/Constellar_Pleiades", 5)]
        public void Given_A_Valid_Monster_Card_Profile_Url_Should_Extract_Card_Rank(string cardProfileUrl, int expected)
        {
            // Arrange
            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetIntValue(CardHtmlTable.Rank);

            // Assert
            result.Should().Be(expected);
        }


        [TestCase("http://yugioh.wikia.com/wiki/Summoner_Monk", "800 / 1600")]
        [TestCase("http://yugioh.wikia.com/wiki/Nobledragon_Magician", "700 / 1400")]
        public void Given_A_Valid_Monster_Card_Profile_Url_Should_Extract_Card_AtkAndDef(string cardProfileUrl, string expected)
        {
            // Arrange

            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetValue(CardHtmlTable.AtkAndDef);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Twin_Triangle_Dragon", "Bottom , Right")]
        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon", "Left , Bottom-Left , Bottom-Right , Right")]
        public void Given_A_Valid_Monster_Card_Profile_Url_Should_Extract_Card_LinkArrows(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetValue(CardHtmlTable.LinkArrows);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }


        [TestCase("http://yugioh.wikia.com/wiki/Twin_Triangle_Dragon", "1200 / 2")]
        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon", "3000 / 4")]
        public void Given_A_Valid_Monster_Card_Profile_Url_Should_Extract_Card_AtkAndLink(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetValue(CardHtmlTable.AtkAndLink);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }


        [TestCase("http://yugioh.wikia.com/wiki/Summoner_Monk", "Spellcaster / Effect")]
        [TestCase("http://yugioh.wikia.com/wiki/Nobledragon_Magician", "Spellcaster / Pendulum / Tuner / Effect")]
        public void Given_A_Valid_Monster_Card_Profile_Url_Should_Extract_Card_Types(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            var result = _sut.GetValue(CardHtmlTable.Types);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Summoner_Monk", "https://vignette.wikia.nocookie.net/yugioh/images/c/c7/SummonerMonk-SDCL-EN-C-1E.png/revision/latest/scale-to-width-down/300?cb=20171107114314")]
        [TestCase("http://yugioh.wikia.com/wiki/Nobledragon_Magician", "https://vignette.wikia.nocookie.net/yugioh/images/9/97/NobledragonMagician-LEDD-EN-C-1E.png/revision/latest/scale-to-width-down/300?cb=20171005220221")]
        public void Given_A_Valid_Monster_Card_Profile_Url_Should_Extract_Card_Profile_Image_Url(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new CardHtmlDocument(new HtmlWebPage());
            htmlDocument.Load(cardProfileUrl);

            _sut.Load(htmlDocument.ProfileElement());

            // Act
            //var result = _sut.(CardHtmlTable.Types);

            // Assert
            //result.Should().BeEquivalentTo(expected);
        }

    }
}