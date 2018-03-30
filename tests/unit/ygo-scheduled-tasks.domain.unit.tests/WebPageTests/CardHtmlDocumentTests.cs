using FluentAssertions;
using HtmlAgilityPack;
using NSubstitute;
using NUnit.Framework;
using System;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Cards;

namespace ygo_scheduled_tasks.domain.unit.tests.WebPageTests
{
    [TestFixture]
    public class CardHtmlDocumentTests
    {
        private ICardHtmlDocument _sut;
        private IHtmlWebPage _htmlWebPage;

        [SetUp]
        public void Setup()
        {
            _htmlWebPage = Substitute.For<IHtmlWebPage>();
            _sut = new CardHtmlDocument(_htmlWebPage);
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

        [Test]
        public void Given_A_Valid_Card_String_Url_Should_Invoke_HtmlWebPage_Load()
        {
            // Arrange
            const string url = "http://www.google.co.uk";
            _htmlWebPage.Load(Arg.Any<string>()).Returns(new HtmlDocument());

            // Act
            _sut.Load(url);

            // Assert
            _htmlWebPage.Received(1).Load(Arg.Any<string>());
        }

        [Test]
        public void Given_A_Valid_Card_Uri_Should_Invoke_HtmlWebPage_Load()
        {
            // Arrange
            var url = new Uri("http://www.google.co.uk");
        
            _htmlWebPage.Load(Arg.Any<Uri>()).Returns(new HtmlDocument());

            // Act
            _sut.Load(url);

            // Assert
            _htmlWebPage.Received(1).Load(Arg.Any<Uri>());
        }

    }
}