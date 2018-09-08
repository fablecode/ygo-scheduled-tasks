using FluentAssertions;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Cards;

namespace ygo_scheduled_tasks.domain.integration.tests.WebPageTests
{
    [TestFixture]
    public class CardHtmlDocumentTests
    {
        private CardHtmlDocument _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CardHtmlDocument(new CardHtmlTable());
        }


        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon", "https://vignette.wikia.nocookie.net/yugioh/images/f/ff/BorreloadDragon-CIBR-EN-ScR-1E.png")]
        [TestCase("http://yugioh.wikia.com/wiki/Twin_Triangle_Dragon", "https://vignette.wikia.nocookie.net/yugioh/images/5/57/TwinTriangleDragon-OP08-EN-SR-UE.png")]
        [TestCase("http://yugioh.wikia.com/wiki/Blue-Eyes_Ultimate_Dragon", "https://vignette.wikia.nocookie.net/yugioh/images/a/ad/BlueEyesUltimateDragon-LCKC-EN-ScR-1E.png")]
        public void Given_A_Card_HtmlDocument_Should_Return_Profile_Image_Url(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.ImageUrl(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon", "Borreload Dragon")]
        [TestCase("http://yugioh.wikia.com/wiki/Twin_Triangle_Dragon", "Twin Triangle Dragon")]
        [TestCase("http://yugioh.wikia.com/wiki/Blue-Eyes_Ultimate_Dragon", "Blue-Eyes Ultimate Dragon")]
        public void Given_A_Card_HtmlDocument_Should_Return_Name(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.Name(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon")]
        [TestCase("http://yugioh.wikia.com/wiki/Twin_Triangle_Dragon")]
        [TestCase("http://yugioh.wikia.com/wiki/Blue-Eyes_Ultimate_Dragon")]
        public void Given_A_Card_HtmlDocument_Should_Return_Description(string cardProfileUrl)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.Description(htmlDocument);

            // Assert
            result.Should().NotBeNullOrWhiteSpace();
        }

        [TestCase("http://yugioh.wikia.com/wiki/Assault_Blackwing_-_Kunifusa_the_White_Rainbow")]
        [TestCase("http://yugioh.wikia.com/wiki/Abyss_Actor%27s_Injury")]
        [TestCase("http://yugioh.wikia.com/wiki/Acrobatic_Circus")]
        public void Given_A_Card_HtmlDocument_With_No_Description_Should_Return_Empty_String(string cardProfileUrl)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.Description(htmlDocument);

            // Assert
            result.Should().BeEmpty();
        }


        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon", "31833038")]
        [TestCase("http://yugioh.wikia.com/wiki/Twin_Triangle_Dragon", "66015185")]
        [TestCase("http://yugioh.wikia.com/wiki/Blue-Eyes_Ultimate_Dragon", "23995346")]
        public void Given_A_Card_HtmlDocument_Should_Return_CardNumber(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.CardNumber(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon", "Monster")]
        [TestCase("http://yugioh.wikia.com/wiki/Twin_Triangle_Dragon", "Monster")]
        [TestCase("http://yugioh.wikia.com/wiki/Blue-Eyes_Ultimate_Dragon", "Monster")]
        public void Given_A_Card_HtmlDocument_Should_Return_CardType(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.CardType(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Advance_Draw", "Normal")]
        [TestCase("http://yugioh.wikia.com/wiki/Combination_Attack", "Quick-Play")]
        [TestCase("http://yugioh.wikia.com/wiki/Blaze_Accelerator", "Continuous")]
        [TestCase("http://yugioh.wikia.com/wiki/Advanced_Ritual_Art", "Ritual")]
        [TestCase("http://yugioh.wikia.com/wiki/Dark_Energy", "Equip")]
        [TestCase("http://yugioh.wikia.com/wiki/Cynet_Storm", "Field")]
        public void Given_A_Card_HtmlDocument_Should_Return_Spell_Property(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.Property(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Arsenal_Robber", "Normal")]
        [TestCase("http://yugioh.wikia.com/wiki/Castle_of_Dragon_Souls", "Continuous")]
        [TestCase("http://yugioh.wikia.com/wiki/Horn_of_Heaven", "Counter")]
        public void Given_A_Card_HtmlDocument_Should_Return_Trap_Property(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.Property(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon", "DARK")]
        [TestCase("http://yugioh.wikia.com/wiki/Amorphage_Envy", "EARTH")]
        [TestCase("http://yugioh.wikia.com/wiki/Bright_Star_Dragon", "LIGHT")]
        public void Given_A_Card_HtmlDocument_Should_Return_Attribute(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.Attribute(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Amorphage_Envy", 4)]
        [TestCase("http://yugioh.wikia.com/wiki/Vampire_Dragon", 5)]
        public void Given_A_Card_HtmlDocument_Should_Return_Level(string cardProfileUrl, int expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.Level(htmlDocument);

            // Assert
            result.Should().Be(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Bujinki_Amaterasu", 4)]
        [TestCase("http://yugioh.wikia.com/wiki/Coach_King_Giantrainer", 8)]
        public void Given_A_Card_HtmlDocument_Should_Return_Rank(string cardProfileUrl, int expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.Rank(htmlDocument);

            // Assert
            result.Should().Be(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Bujinki_Amaterasu", "2600 / 2500")]
        [TestCase("http://yugioh.wikia.com/wiki/Coach_King_Giantrainer", "2800 / 2000")]
        public void Given_A_Card_HtmlDocument_Should_Return_AtkDef(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.AtkDef(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Cyberse_Accelerator", "2000 / 3")]
        [TestCase("http://yugioh.wikia.com/wiki/Fresh_Madolche_Sistart", "1500 / 2")]
        public void Given_A_Card_HtmlDocument_Should_Return_AtkLink(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.AtkLink(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Twin_Triangle_Dragon", "Bottom , Right")]
        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon", "Left , Bottom-Left , Bottom-Right , Right")]
        public void Given_A_Valid_Monster_Card_Profile_Url_Should_Extract_Card_LinkArrows(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.LinkArrows(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }


        [TestCase("http://yugioh.wikia.com/wiki/Cyberse_Accelerator", "Cyberse / Link / Effect")]
        [TestCase("http://yugioh.wikia.com/wiki/Fresh_Madolche_Sistart", "Fairy / Link / Effect")]
        [TestCase("http://yugioh.wikia.com/wiki/Summoner_Monk", "Spellcaster / Effect")]
        [TestCase("http://yugioh.wikia.com/wiki/Nobledragon_Magician", "Spellcaster / Pendulum / Tuner / Effect")]
        public void Given_A_Card_HtmlDocument_Should_Return_Types(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.Types(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Cyberse_Accelerator", "2+ monsters, except Tokens")]
        [TestCase("http://yugioh.wikia.com/wiki/Fresh_Madolche_Sistart", "2 \"Madolche\" monsters")]
        public void Given_A_Card_HtmlDocument_Should_Return_Materials(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.Materials(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [TestCase("http://yugioh.wikia.com/wiki/Cyberse_Accelerator", "Quick,Condition")]
        [TestCase("http://yugioh.wikia.com/wiki/Fresh_Madolche_Sistart", "Continuous,Continuous")]
        public void Given_A_Card_HtmlDocument_Should_Return_CardEffectTypes(string cardProfileUrl, string expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.CardEffectTypes(htmlDocument);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
        [TestCase("http://yugioh.wikia.com/wiki/Black_Fang_Magician", 8)]
        [TestCase("http://yugioh.wikia.com/wiki/Clear_Wing_Fast_Dragon", 4)]
        public void Given_A_Card_HtmlDocument_Should_Return_PendulumScale(string cardProfileUrl, long expected)
        {
            // Arrange
            var htmlDocument = new HtmlWebPage().Load(cardProfileUrl);

            // Act
            var result = _sut.PendulumScale(htmlDocument);

            // Assert
            result.Should().Be(expected);
        }

    }
}