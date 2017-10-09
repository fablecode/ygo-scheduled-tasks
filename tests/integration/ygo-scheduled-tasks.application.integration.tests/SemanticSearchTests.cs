using System;
using System.Configuration;
using FluentAssertions;
using NUnit.Framework;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.application.integration.tests
{
    [TestFixture]
    public class SemanticSearchTests
    {
        private SemanticSearch _sut;

        [SetUp]
        public void Setup()
        {
            var config = (Config) ConfigurationManager.GetSection("ygo-settings");

            _sut = new SemanticSearch(config, new HtmlWebPage());
        }

        [TestCase(SemanticSearchUrls.NormalMonsterCardsSearch)]
        public void Given_A_Card_SemanticSearch_Url_SearchResults_Should_Not_Empty(string searchUrl)
        {
            // Arrange

            // Act
            var results = _sut.CardsByUrl(searchUrl);

            // Assert
            results.Should().NotBeEmpty();
        }

        [TestCase(SemanticSearchUrls.NormalMonsterCardsSearch, "Pendulum Machine")]
        public void Given_A_Card_SemanticSearch_Url_SearchResults_Should_Contain_Specific_Card(string searchUrl, string expected)
        {
            // Arrange
            // Act
            var results = _sut.CardsByUrl(searchUrl);

            // Assert
            results.Should().ContainSingle(sc => sc.Name.Equals(expected, StringComparison.OrdinalIgnoreCase));
        }
    }
}