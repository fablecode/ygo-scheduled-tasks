using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.domain.unit.tests.ProcessorTests.ItemTests
{
    [TestFixture]
    public class CardItemProcessorTests
    {
        private CardItemProcessor _sut;
        private ICardWebPage _cardWebPage;
        private IConfig _config;
        private IYugiohCardService _yugiohCardService;

        [SetUp]
        public void Setup()
        {
            _config = Substitute.For<IConfig>();
            _cardWebPage = Substitute.For<ICardWebPage>();
            _yugiohCardService = Substitute.For<IYugiohCardService>();

            _sut = new CardItemProcessor(_config, _cardWebPage, _yugiohCardService);

        }

        [TestCase("TCG cards")]
        [TestCase("OCG cards")]
        public void Should_Only_Handle_Category(string category)
        {
            // Arrange

            // Act
            var result = _sut.Handles(category);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task Given_A_Valid_Article_Should_Execute_GetYugiohCard()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _cardWebPage.GetYugiohCard(Arg.Any<string>()).Returns((YugiohCard)null);

            // Act
            await _sut.ProcessItem(article);

            // Assert
            _cardWebPage.Received(1).GetYugiohCard(Arg.Any<Uri>());
        }

        [Test]
        public async Task Given_A_Valid_Article_And_YugiohCard_Info_Is_Extracted_Should_Execute_AddOrUpdate()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _cardWebPage.GetYugiohCard(Arg.Any<Uri>()).Returns(new YugiohCard());
            _yugiohCardService.AddOrUpdate(Arg.Any<YugiohCard>()).Returns((Card)null);

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _yugiohCardService.Received(1).AddOrUpdate(Arg.Any<YugiohCard>());
        }

        [Test]
        public async Task Given_A_Valid_Article_And_YugiohCard_Info_Is_Extracted_IsSuccessful_Should_BeTrue()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _cardWebPage.GetYugiohCard(Arg.Any<Uri>()).Returns(new YugiohCard());
            _yugiohCardService.AddOrUpdate(Arg.Any<YugiohCard>()).Returns(new Card());

            // Act
            var result = await _sut.ProcessItem(article);

            // Assert
            result.IsSuccessfullyProcessed.Should().BeTrue();
        }

        [Test]
        public async Task Given_A_Valid_Article_And_YugiohCard_Info_Is_Extracted_IsSuccessful_Should_BeFalse()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "Blue-Eyes", Url = "/wiki/Blue-Eyes" };

            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");
            _cardWebPage.GetYugiohCard(Arg.Any<Uri>()).Returns(new YugiohCard());
            _yugiohCardService.AddOrUpdate(Arg.Any<YugiohCard>()).Returns((Card)null);

            // Act
            var result = await _sut.ProcessItem(article);

            // Assert
            result.IsSuccessfullyProcessed.Should().BeFalse();
        }
    }
}