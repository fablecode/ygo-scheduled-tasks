using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item;
using ygo_scheduled_tasks.domain.Services;
using ygo_scheduled_tasks.domain.WebPage.Archetypes;

namespace ygo_scheduled_tasks.domain.unit.tests.ProcessorTests.ItemTests
{
    [TestFixture]
    public class CardsByArchetypeItemProcessorTests
    {
        private CardsByArchetypeItemProcessor _sut;
        private IConfig _config;
        private IArchetypeService _archetypeService;
        private IArchetypeCardsService _archetypeCardsService;
        private IArchetypeWebPage _archetypeWebPage;

        [SetUp]
        public void Setup()
        {
            _archetypeWebPage = Substitute.For<IArchetypeWebPage>();
            _archetypeService = Substitute.For<IArchetypeService>();
            _archetypeCardsService = Substitute.For<IArchetypeCardsService>();
            _config = Substitute.For<IConfig>();

            _sut = new CardsByArchetypeItemProcessor(_archetypeWebPage, _archetypeService, _archetypeCardsService, _config);
        }

        [Test]
        public void Should_Only_Handle_Cards_By_Archetype()
        {
            // Arrange

            // Act
            var result = _sut.Handles("Cards by archetype");

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task Given_A_CardsByArchetype_Article_Should_Execute_ArchetypeByName()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Clear Wing", Url = "/wiki/List_of_\"Clear_Wing\"_cards" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeService.ArchetypeByName(Arg.Any<string>()).Returns((Archetype)null);
            _archetypeService.Add(Arg.Any<AddArchetypeCommand>()).Returns((Archetype)null);

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.Received(1).ArchetypeByName(Arg.Any<string>());
        }

        [Test]
        public async Task Given_A_CardsByArchetype_Article_Should_Execute_ArchetypeWebPage_Cards()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Clear Wing", Url = "/wiki/List_of_\"Clear_Wing\"_cards" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeService.ArchetypeByName(Arg.Any<string>()).Returns(new Archetype());
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeCardsService.Update(Arg.Any<UpdateArchetypeCardsCommand>()).Returns((IEnumerable<ArchetypeCard>)null);

            // Act
            await _sut.ProcessItem(article);

            // Assert
            _archetypeWebPage.Received(1).Cards(Arg.Any<Uri>());
        }

        [Test]
        public async Task Given_A_CardsByArchetype_Article_Should_Execute_ArchetypeCardsService_Update()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Clear Wing", Url = "/wiki/List_of_\"Clear_Wing\"_cards" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeService.ArchetypeByName(Arg.Any<string>()).Returns(new Archetype());
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeCardsService.Update(Arg.Any<UpdateArchetypeCardsCommand>()).Returns((IEnumerable<ArchetypeCard>)null);

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeCardsService.Received(1).Update(Arg.Any<UpdateArchetypeCardsCommand>());
        }

        [Test]
        public async Task Given_A_CardsByArchetype_Article_IsSuccessful_Should_Be_False()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Clear Wing", Url = "/wiki/List_of_\"Clear_Wing\"_cards" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeService.ArchetypeByName(Arg.Any<string>()).Returns(new Archetype());
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeCardsService.Update(Arg.Any<UpdateArchetypeCardsCommand>()).Returns((IEnumerable<ArchetypeCard>)null);

            // Act
            var result = await _sut.ProcessItem(article);

            // Assert
            result.IsSuccessfullyProcessed.Should().BeFalse();
        }

        [Test]
        public async Task Given_A_CardsByArchetype_Article_IsSuccessful_Should_Be_True()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Clear Wing", Url = "/wiki/List_of_\"Clear_Wing\"_cards" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeService.ArchetypeByName(Arg.Any<string>()).Returns(new Archetype());
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeCardsService.Update(Arg.Any<UpdateArchetypeCardsCommand>()).Returns(new List<ArchetypeCard>());

            // Act
            var result = await _sut.ProcessItem(article);

            // Assert
            result.IsSuccessfullyProcessed.Should().BeTrue();
        }


        [Test]
        public async Task Given_An_ArchetypeCard_Article_Should_Execute_Update()
        {
            // Arrange
            var article = new UnexpandedArticle {Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes"};

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeService.ArchetypeByName(Arg.Any<string>()).Returns(new Archetype());
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> {"Blue-Eyes White Dragon"});
            _archetypeCardsService.Update(Arg.Any<UpdateArchetypeCardsCommand>()).Returns(new List<ArchetypeCard>());
            
            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeCardsService.Received(1).Update(Arg.Any<UpdateArchetypeCardsCommand>());
        }

        [Test]
        public async Task Given_A_Archetype_Article_Should_Not_Execute_ServiceMethod_Update()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Add(Arg.Any<AddArchetypeCommand>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.DidNotReceive().Update(Arg.Any<UpdateArchetypeCommand>());
        }
    }
}