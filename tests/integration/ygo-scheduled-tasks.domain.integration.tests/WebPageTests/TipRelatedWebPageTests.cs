using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using HtmlAgilityPack;
using NSubstitute;
using NUnit.Framework;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;
using ygo_scheduled_tasks.domain.WebPage.Cards.Tips;

namespace ygo_scheduled_tasks.domain.integration.tests.WebPageTests
{
    [TestFixture]
    public class TipRelatedWebPageTests
    {
        private TipRelatedWebPage _sut;
        private ITipRelatedCardList _tipRelatedCardList;
        private ISemanticSearch _semanticSearch;
        private IConfig _config;

        [SetUp]
        public void Setup()
        {
            _config = Substitute.For<IConfig>();
            _tipRelatedCardList = Substitute.For<ITipRelatedCardList>();
            var tipRelatedHtmlDocument = Substitute.For<ITipRelatedHtmlDocument>();
            _semanticSearch = Substitute.For<ISemanticSearch>();

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");

            _sut = new TipRelatedWebPage(_config, _tipRelatedCardList, tipRelatedHtmlDocument, _semanticSearch);
        }

        [Test]
        public void Given_A_Null_CardTipSection_Should_Throw_ArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => _sut.GetTipRelatedCards(null, new UnexpandedArticle{ Url = "/wiki/Card_Tips:Sangan" });

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_A_Null_UnexpandedArticle_Should_Throw_ArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => _sut.GetTipRelatedCards(null, new UnexpandedArticle());

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_A_Null_HtmlNode_Should_Throw_ArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => _sut.GetTipRelatedCards(new CardTipSection(), "http:someurl", null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Given_A_Null_CardTipSection_With_TipRelatedUrl_Should_Throw_ArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => _sut.GetTipRelatedCards(null, null, null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestCase("http://yugioh.wikia.com/wiki/Card_Tips:Sangan")]
        public void Given_A_CardTipSection_And_TipRelatedCardListTableWith_Should_Invoke_ExtractCardsFromTable(string url)
        {
            // Arrange
            var fixture = new Fixture {RepeatCount = 10};
            _tipRelatedCardList.ExtractCardsFromTable(Arg.Any<HtmlNode>()).Returns(fixture.Create<List<string>>());
            var htmlDocument = new HtmlWeb().Load(url);
            var htmlTable = new TipRelatedHtmlDocument(_config).GetTable(htmlDocument);
            var cardTipSection = new CardTipSection();

            // Act
            _sut.GetTipRelatedCards(cardTipSection, null, htmlTable);

            // Assert
            _tipRelatedCardList.Received(1).ExtractCardsFromTable(Arg.Any<HtmlNode>());
        }

        [TestCase("http://yugioh.wikia.com/wiki/Card_Tips:Sangan")]
        public void Given_A_CardTipSection_And_TipRelatedCardListTableWith_Should_Not_Invoke_ExtractCardsFromTable(string url)
        {
            // Arrange
            var fixture = new Fixture { RepeatCount = 10 };
            _semanticSearch.CardsByUrl(Arg.Any<string>()).Returns(fixture.Create<List<SemanticCard>>());
            var htmlDocument = new HtmlWeb().Load(url);
            var htmlTable = new TipRelatedHtmlDocument(_config).GetTable(htmlDocument);
            var cardTipSection = new CardTipSection();

            // Act
            _sut.GetTipRelatedCards(cardTipSection, url, htmlTable);

            // Assert
            _tipRelatedCardList.DidNotReceive().ExtractCardsFromTable(Arg.Any<HtmlNode>());
        }

        [TestCase("http://yugioh.wikia.com/wiki/Card_Tips:Sangan")]
        public void Given_A_CardTipSection_And_TipRelatedCardListTableWith_Should_Invoke_CardsByUrl(string url)
        {
            // Arrange
            var fixture = new Fixture { RepeatCount = 10 };
            _semanticSearch.CardsByUrl(Arg.Any<string>()).Returns(fixture.Create<List<SemanticCard>>());
            var htmlDocument = new HtmlWeb().Load(url);
            var htmlTable = new TipRelatedHtmlDocument(_config).GetTable(htmlDocument);
            var cardTipSection = new CardTipSection();

            // Act
            _sut.GetTipRelatedCards(cardTipSection, url, htmlTable);

            // Assert
            _semanticSearch.Received(1).CardsByUrl(Arg.Any<string>());
        }

        [TestCase("http://yugioh.wikia.com/wiki/Card_Tips:Sangan")]
        public void Given_A_CardTipSection_And_TipRelatedCardListTableWith_Should_Not_Invoke_CardsByUrl(string url)
        {
            // Arrange
            var fixture = new Fixture { RepeatCount = 10 };
            _tipRelatedCardList.ExtractCardsFromTable(Arg.Any<HtmlNode>()).Returns(fixture.Create<List<string>>());
            var htmlDocument = new HtmlWeb().Load(url);
            var htmlTable = new TipRelatedHtmlDocument(_config).GetTable(htmlDocument);
            var cardTipSection = new CardTipSection();

            // Act
            _sut.GetTipRelatedCards(cardTipSection, null, htmlTable);

            // Assert
            _semanticSearch.DidNotReceive().CardsByUrl(Arg.Any<string>());
        }
    }
}