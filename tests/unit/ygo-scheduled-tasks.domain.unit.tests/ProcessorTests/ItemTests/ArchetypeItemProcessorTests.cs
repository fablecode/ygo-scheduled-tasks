using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item;
using ygo_scheduled_tasks.domain.Services;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.domain.unit.tests.ProcessorTests.ItemTests
{
    [TestFixture]
    public class ArchetypeItemProcessorTests
    {
        private ArchetypeItemProcessor _sut;
        private IConfig _config;
        private IArchetypeService _archetypeService;
        private IArchetypeWebPage _archetypeWebPage;

        [SetUp]
        public void Setup()
        {
            _archetypeWebPage = Substitute.For<IArchetypeWebPage>();
            _archetypeService = Substitute.For<IArchetypeService>();
            _config = Substitute.For<IConfig>();

            _sut = new ArchetypeItemProcessor(_archetypeWebPage, _archetypeService, _config);
        }

        [Test]
        public async Task Given_An_Archetype_Article_Should_Execute_ArchetypeWebPage_Cards()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Add(Arg.Any<YugiohArchetype>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            _archetypeWebPage.Received(1).Cards(Arg.Any<Uri>());
        }


        [Test]
        public async Task Given_An_Archetype_Article_Should_Execute_ArchetypeByName()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Add(Arg.Any<YugiohArchetype>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.Received(1).ArchetypeByName(Arg.Any<string>());
        }


        [Test]
        public async Task Given_A_New_Archetype_Article_Should_Execute_Add_ServiceMethod()
        {
            // Arrange
            var article = new UnexpandedArticle {Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes"};

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> {"Blue-Eyes White Dragon"});
            _archetypeService.Add(Arg.Any<YugiohArchetype>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.Received(1).Add(Arg.Any<YugiohArchetype>());
        }

        [Test]
        public async Task Given_A_New_Archetype_Article_Should_Not_Execute_Update_ServiceMethod()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Add(Arg.Any<YugiohArchetype>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.DidNotReceive().Update(Arg.Any<YugiohArchetype>());
        }

        [Test]
        public async Task Given_An_Existing_Archetype_Article_Should_Execute_Update_ServiceMethod()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Update(Arg.Any<YugiohArchetype>()).Returns(new Archetype());
            _archetypeService.ArchetypeByName(Arg.Any<string>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.Received(1).Update(Arg.Any<YugiohArchetype>());
        }

        [Test]
        public async Task Given_An_Existing_Archetype_Article_Should_Not_Execute_Add_ServiceMethod()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Update(Arg.Any<YugiohArchetype>()).Returns(new Archetype());
            _archetypeService.ArchetypeByName(Arg.Any<string>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.DidNotReceive().Add(Arg.Any<YugiohArchetype>());
        }

    }
}