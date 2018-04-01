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
        public void Should_Only_Handle_Forbidden_And_Limited_Category()
        {
            // Arrange

            // Act
            var result = _sut.Handles("Archetypes");

            // Assert
            result.Should().BeTrue();
        }


        [Test]
        public async Task Given_An_Archetype_Article_Should_Execute_ArchetypeWebPage_Cards()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Add(Arg.Any<AddArchetypeCommand>()).Returns(new Archetype());

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
            _archetypeService.Add(Arg.Any<AddArchetypeCommand>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.Received(1).ArchetypeById(Arg.Any<int>());
        }


        [Test]
        public async Task Given_A_New_Archetype_Article_Should_Execute_Add_ServiceMethod()
        {
            // Arrange
            var article = new UnexpandedArticle {Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes"};

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> {"Blue-Eyes White Dragon"});
            _archetypeService.Add(Arg.Any<AddArchetypeCommand>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.Received(1).Add(Arg.Any<AddArchetypeCommand>());
        }

        [Test]
        public async Task Given_A_New_Archetype_Article_Should_Not_Execute_Update_ServiceMethod()
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

        [Test]
        public async Task Given_An_Existing_Archetype_Article_Should_Execute_Update_ServiceMethod()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Update(Arg.Any<UpdateArchetypeCommand>()).Returns(new Archetype());
            _archetypeService.ArchetypeById(Arg.Any<int>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.Received(1).Update(Arg.Any<UpdateArchetypeCommand>());
        }

        [Test]
        public async Task Given_An_Existing_Archetype_Article_Should_Not_Execute_Add_ServiceMethod()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Update(Arg.Any<UpdateArchetypeCommand>()).Returns(new Archetype());
            _archetypeService.ArchetypeById(Arg.Any<int>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.DidNotReceive().Add(Arg.Any<AddArchetypeCommand>());
        }

        [Test]
        public async Task Given_An_Archetype_If_Title_Equals_Archetype_Should_Not_Execute_ArchetypeThumbnail()
        {
            // Arrange  
            var article = new UnexpandedArticle { Title = "Archetype", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Add(Arg.Any<AddArchetypeCommand>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeWebPage.DidNotReceive().ArchetypeThumbnail(Arg.Any<int>(), Arg.Any<string>());
        }

        [Test]
        public async Task Given_An_Archetype_If_Title_Equals_Archetype_Should_Not_Execute_ArchetypeById()
        {
            // Arrange  
            var article = new UnexpandedArticle { Title = "Archetype", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Add(Arg.Any<AddArchetypeCommand>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.DidNotReceive().ArchetypeById(Arg.Any<long>());
        }

        [Test]
        public async Task Given_An_Archetype_If_Title_Equals_Archetype_Should_Not_Execute_Add_ServiceMethod()
        {
            // Arrange  
            var article = new UnexpandedArticle { Title = "Archetype", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Add(Arg.Any<AddArchetypeCommand>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.DidNotReceive().Add(Arg.Any<AddArchetypeCommand>());
        }

        [Test]
        public async Task Given_An_Archetype_If_Title_Equals_Archetype_Should_Not_Execute_Update_ServiceMethod()
        {
            // Arrange  
            var article = new UnexpandedArticle { Title = "Archetype", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Update(Arg.Any<UpdateArchetypeCommand>()).Returns(new Archetype());

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _archetypeService.DidNotReceive().Update(Arg.Any<UpdateArchetypeCommand>());
        }

        [Test]
        public async Task Given_An_Archetype_If_Title_Equals_Archetype_IsSuccessfullyProcessed_Should_Be_False()
        {
            // Arrange  
            var article = new UnexpandedArticle { Title = "Archetype", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _archetypeWebPage.Cards(Arg.Any<Uri>()).Returns(new List<string> { "Blue-Eyes White Dragon" });
            _archetypeService.Update(Arg.Any<UpdateArchetypeCommand>()).Returns(new Archetype());

            // Act
            var result = await _sut.ProcessItem(article);

            // Assert
            result.IsSuccessfullyProcessed.Should().BeFalse();
        }

    }
}