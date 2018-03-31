using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Process;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.domain.integration.tests.ETLTests.SemanticSearchTests.ProcessorTests.ProcessTests
{
    public class SemanticSearchNormalMonstersProcessorTests
    {
        private SemanticSearchNormalMonstersProcessor _sut;
        private IConfig _config;
        private ICardWebPage _cardWebPage;
        private IYugiohCardService _yugiohCardService;

        [SetUp]
        public void Setup()
        {
            _config = Substitute.For<IConfig>();
            _cardWebPage = Substitute.For<ICardWebPage>();
            _yugiohCardService = Substitute.For<IYugiohCardService>();

            _sut = new SemanticSearchNormalMonstersProcessor(_config, _cardWebPage, _yugiohCardService);
        }

        [Test]
        public void Should_Only_Handle_SemanticSearch_For_Normal_Monsters()
        {
            // Arrange

            // Act
            var result = _sut.Handles("NormalMonsters");

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task Given_An_Invalid_Normal_Monster_Url_IsSuccessfullyProcessed_Should_Be_False()
        {
            // Arrange
            _config.WikiaDomainUrl.Returns("http://www.yugioh.wikia.com");
            _cardWebPage.GetYugiohCard(Arg.Any<Uri>()).Returns((YugiohCard)null);

            // Act
            var result = await _sut.ProcessItem(new SemanticCard { Name = "NotACard", Url = "/wiki/Archetype" });

            // Assert
            result.IsSuccessfullyProcessed.Should().BeFalse();
        }

        [Test]
        public async Task Given_A_Valid_Normal_Monster_Url_If_Types_Does_Not_Contain_Normal_Should_Append_Normal()
        {
            // Arrange
            _config.WikiaDomainUrl.Returns("http://www.yugioh.wikia.com");
            _cardWebPage.GetYugiohCard(Arg.Any<Uri>()).Returns(new YugiohCard { Types = "Dragon" });

            // Act
            var result = await _sut.ProcessItem(new SemanticCard { Name = "Blue-Eyes White Dragon", Url = "/wiki/Blue-Eyes_White_Dragon" });

            // Assert
            result.YugiohCard.Types.Should().Contain("Normal");
        }

        [Test]
        public async Task Given_A_Valid_Normal_Monster_Types_Should_Contain_Only_One_Instance_Of_Normal()
        {
            // Arrange
            _config.WikiaDomainUrl.Returns("http://www.yugioh.wikia.com");
            _cardWebPage.GetYugiohCard(Arg.Any<Uri>()).Returns(new YugiohCard { Types = "Dragon" });

            // Act
            var result = await _sut.ProcessItem(new SemanticCard { Name = "Blue-Eyes White Dragon", Url = "/wiki/Blue-Eyes_White_Dragon" });

            // Assert
            result.YugiohCard.Types.Split(new[]{" / " }, StringSplitOptions.None).Should().Contain("Normal");
        }


    }
}