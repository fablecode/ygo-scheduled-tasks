using System;
using System.Collections.Generic;
using FluentAssertions;
using HtmlAgilityPack;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.services.WebPage;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.domain.services.integration.tests.WebPageTests
{
    [TestFixture]
    public class CardWebPageTests
    {
        [SetUp]
        public void Setup()
        {
            var sut = new CardWebPage(new HtmlWebPage());
        }
    }

    public class CardWebPage
    {
        private readonly IHtmlWebPage _htmlWebPage;
        private HtmlDocument _cardPage;

        public CardWebPage(IHtmlWebPage htmlWebPage)
        {
            _htmlWebPage = htmlWebPage;
        }

        public void Load(string url)
        {
            Load(new Uri(url));
        }

        public void Load(Uri url)
        {
            _cardPage = _htmlWebPage.Load(url.AbsoluteUri);
        }
    }

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

    }

    public class CardHtmlDocument
    {
        private readonly IHtmlWebPage _htmlWebPage;
        private HtmlDocument _cardPage;

        public CardHtmlDocument(IHtmlWebPage htmlWebPage)
        {
            _htmlWebPage = htmlWebPage;
        }

        public void Load(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(nameof(url));

            Load(new Uri(url));
        }

        public void Load(Uri url)
        {
            _cardPage = _htmlWebPage.Load(url.AbsoluteUri);
        }

        public HtmlNode ProfileElement()
        {
            return _cardPage.DocumentNode.SelectSingleNode("//div[@id='WikiaArticle']//table[contains(@class, 'cardtable')]");
        }

        public string ProfileImageUrl()
        {
            var imageUrl = _cardPage.DocumentNode.SelectSingleNode("//td[@class='cardtable-cardimage']/a/img").Attributes["src"].Value;

            if (imageUrl.Contains("revision"))
                imageUrl = imageUrl.Substring(0, imageUrl.IndexOf("/revision", StringComparison.Ordinal));

            return imageUrl;
        }
    }

    [TestFixture]
    public class CardWebPageHelperTests
    {
        [SetUp]
        public void Setup()
        {
            
        }
    }

    public static class CardWebPageHelper
    {
        
    }

    public sealed class YugiohCard
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }
        public int Level { get; set; }
        public int Rank { get; set; }
        public long PendulumScale { get; set; }
        public string AtkDef { get; set; }
        public string CardNumber { get; set; }
        public string Materials { get; set; }
        public List<string> CardEffectTypes { get; set; }
        public string Property { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> LinkArrows { get; set; }
    }
}