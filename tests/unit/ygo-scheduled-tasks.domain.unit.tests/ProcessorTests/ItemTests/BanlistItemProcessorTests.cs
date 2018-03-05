﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using wikia.Api;
using wikia.Models.Article.AlphabeticalList;
using wikia.Models.Article.Details;
using wikia.Models.Article.Simple;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.domain.unit.tests.ProcessorTests.ItemTests
{
    [TestFixture]
    public class BanlistItemProcessorTests
    {
        private IYugiohBanlistService _yugiohBanlistService;
        private IWikiArticle _wikiArticle;
        private BanlistItemProcessor _sut;

        [SetUp]
        public void SetUp()
        {
            _wikiArticle = Substitute.For<IWikiArticle>();
            _yugiohBanlistService = Substitute.For<IYugiohBanlistService>();

            _sut = new BanlistItemProcessor(_wikiArticle, _yugiohBanlistService);
        }

        [Test]
        public async Task Given_An_Invalid_Banlist_Article_Should_Not_Execute_Update_AddOrUpdate_Method()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "January 2018 Lists", Url = "/wiki/January_2018_Lists" };

            var expandedArticleResultSet = new ExpandedArticleResultSet { Items = new Dictionary<string, ExpandedArticle>()};
            expandedArticleResultSet.Items.Add("test", new ExpandedArticle { Id = 23422, Abstract = string.Empty});
            _wikiArticle.Details(Arg.Any<int>()).Returns(expandedArticleResultSet);

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _yugiohBanlistService.DidNotReceive().AddOrUpdate(Arg.Any<YugiohBanlist>());
        }

        [Test]
        public async Task Given_An_Invalid_Banlist_Article_Should_Not_Execute_WikiArticle_Simple_Method()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "January 2018 Lists", Url = "/wiki/January_2018_Lists" };

            var expandedArticleResultSet = new ExpandedArticleResultSet { Items = new Dictionary<string, ExpandedArticle>() };
            expandedArticleResultSet.Items.Add("test", new ExpandedArticle { Id = 23422, Abstract = string.Empty });
            _wikiArticle.Details(Arg.Any<int>()).Returns(expandedArticleResultSet);

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _wikiArticle.DidNotReceive().Simple(Arg.Any<int>());
        }


        [Test]
        public async Task Given_A_Banlist_Article_Should_Execute_WikiArticle_Simple_Method()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "January 2018 Lists", Url = "/wiki/January_2018_Lists" };

            var expandedArticleResultSet = new ExpandedArticleResultSet { Items = new Dictionary<string, ExpandedArticle>() };
            expandedArticleResultSet.Items.Add("test", new ExpandedArticle { Id = 23422, Abstract = "These are the January 2018 Forbidden and Limited Lists for the OCG in effect since January 1, 2018" });
            _wikiArticle.Details(Arg.Any<int>()).Returns(expandedArticleResultSet);
            _wikiArticle.Simple(Arg.Any<int>()).Returns(new ContentResult { Sections = new Section[0]});

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _wikiArticle.Received(1).Simple(Arg.Any<int>());
        }

        [Test]
        public async Task Given_A_Banlist_Article_Should_Execute_Update_AddOrUpdate_Method()
        {
            // Arrange
            var article = new UnexpandedArticle { Title = "January 2018 Lists", Url = "/wiki/January_2018_Lists" };

            var expandedArticleResultSet = new ExpandedArticleResultSet { Items = new Dictionary<string, ExpandedArticle>() };
            expandedArticleResultSet.Items.Add("test", new ExpandedArticle { Id = 23422, Abstract = "These are the January 2018 Forbidden and Limited Lists for the OCG in effect since January 1, 2018" });
            _wikiArticle.Details(Arg.Any<int>()).Returns(expandedArticleResultSet);
            _wikiArticle.Simple(Arg.Any<int>()).Returns(new ContentResult { Sections = new Section[0] });

            // Act
            await _sut.ProcessItem(article);

            // Assert
            await _yugiohBanlistService.Received(1).AddOrUpdate(Arg.Any<YugiohBanlist>());
        }
    }
}