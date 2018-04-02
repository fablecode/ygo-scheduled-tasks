using System;
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
            _sut = new CardHtmlDocument(new HtmlWebPage());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Given_An_Invalid_Card_Profile_Url_Should_Throw_ArgumentException(string cardProfileUrl)
        {
            // Arrange

            // Act
            Action act = () => _sut.Load(cardProfileUrl);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon")]
        [TestCase("http://yugioh.wikia.com/wiki/Twin_Triangle_Dragon")]
        [TestCase("http://yugioh.wikia.com/wiki/Blue-Eyes_Ultimate_Dragon")]
        public void Given_A_Valid_Card_Profile_Url_Should_Return_Profile_HtmlNode(string cardProfileUrl)
        {
            // Arrange
            _sut.Load(cardProfileUrl);

            // Act
            var result = _sut.ProfileElement();

            // Assert
            result.Should().NotBeNull();
        }

        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon")]
        [TestCase("http://yugioh.wikia.com/wiki/Twin_Triangle_Dragon")]
        [TestCase("http://yugioh.wikia.com/wiki/Blue-Eyes_Ultimate_Dragon")]
        public void Given_A_Valid_Card_Profile_Url_Should_Return_Profile_Image_Url(string cardProfileUrl)
        {
            // Arrange
            _sut.Load(cardProfileUrl);

            // Act
            var result = _sut.ProfileImageUrl();

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [TestCase("http://yugioh.wikia.com/wiki/Borreload_Dragon")]
        [TestCase("http://yugioh.wikia.com/wiki/Twin_Triangle_Dragon")]
        [TestCase("http://yugioh.wikia.com/wiki/Blue-Eyes_Ultimate_Dragon")]
        public void Given_A_Valid_Card_Profile_Url_Should_Return_Profile_Card_Description(string cardProfileUrl)
        {
            // Arrange
            _sut.Load(cardProfileUrl);

            // Act
            var result = _sut.ProfileCardDescription();

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [TestCase("http://yugioh.wikia.com/wiki/Assault_Blackwing_-_Kunifusa_the_White_Rainbow")]
        [TestCase("http://yugioh.wikia.com/wiki/Abyss_Actor%27s_Injury")]
        [TestCase("http://yugioh.wikia.com/wiki/Acrobatic_Circus")]
        public void Given_A_Valid_Card_Profile_Url_With_No_Description_Should_Return_Empty_String(string cardProfileUrl)
        {
            // Arrange
            _sut.Load(cardProfileUrl);

            // Act
            var result = _sut.ProfileCardDescription();

            // Assert
            result.Should().BeEmpty();
        }

    }
}