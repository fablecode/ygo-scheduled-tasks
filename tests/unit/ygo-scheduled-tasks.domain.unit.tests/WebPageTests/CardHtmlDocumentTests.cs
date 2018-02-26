using System;
using FluentAssertions;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Card;

namespace ygo_scheduled_tasks.domain.unit.tests.WebPageTests
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
            act.ShouldThrow<ArgumentException>();
        }
    }
}