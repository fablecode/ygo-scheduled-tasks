using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Archetypes;

namespace ygo_scheduled_tasks.domain.integration.tests.WebPageTests
{
    [TestFixture]
    public class ArchetypeWebPageTests
    {
        private IArchetypeWebPage _sut;
        private IConfig _config;

        [SetUp]
        public void Setup()
        {
            _config = Substitute.For<IConfig>();
            _sut = new ArchetypeWebPage(_config, new HtmlWebPage());
        }

        [TestCase("http://yugioh.wikia.com/wiki/List_of_%22Blackwing%22_cards", "/wiki/Special:Ask/-5B-5BCard-20type::Monster-20Card-5D-5D-20-5B-5BArchseries::Blackwing-5D-5D/-3FJapanese-20name/-3FPrimary-20type/-3FSecondary-20type/-3FAttribute%3D-5B-5BAttribute-5D-5D/-3FType%3D-5B-5BType-5D-5D/-3FStars-20string%3D-5B-5BLevel-5D-5D-2F-20-5B-5BRank-5D-5D/-3FATK-20string%3D-5B-5BATK-5D-5D/-3FDEF-20string%3D-5B-5BDEF-5D-5D/mainlabel%3D/limit%3D50/offset%3D50/format%3Dtable/headers%3D-20plain/searchlabel%3D-20...-20further-20results-20(62-20more)/class%3D-20sortable-20wikitable-20smwtable-20card-2Dlist")]
        [TestCase("http://yugioh.wikia.com/wiki/List_of_%22HERO%22_cards", "http://yugioh.wikia.com/wiki/Special:Ask?x=-5B-5BClass-201%3A%3AOfficial-5D-5D-20-5B-5BCard-20type%3A%3AMonster-20Card-5D-5D-20-5B-5BArchseries%3A%3AHERO-5D-5D%2F-3FEnglish-20name-20%28linked%29%3D%2F-3FJapanese-20name%2F-3FPrimary-20type%2F-3FSecondary-20type%2F-3FAttribute%3D-5B-5BAttribute-5D-5D%2F-3FType%3D-5B-5BType-5D-5D%2F-3FStars-20string%3D-5B-5BLevel-5D-5D-2F-20-5B-5BRank-5D-5D%2F-3FATK-20string%3D-5B-5BATK-5D-5D%2F-3FDEF-20string%3D-5B-5BDEF-5D-5D&amp;mainlabel=%20-&amp;limit=50&amp;offset=50&amp;format=table&amp;headers=%20plain&amp;searchlabel=%20...%20further%20results%20%2864%20more%29&amp;class=%20sortable%20wikitable%20smwtable%20card-list")]
        public void Given_An_Archetype_Page_Url_Should_Return_FurtherResultsUrl(string archetypeUrl, string expected)
        {
            // Arrange
            var document = new HtmlWebPage().Load(archetypeUrl);

            // Act
            var result = _sut.GetFurtherResultsUrl(document);

            // Assert
            result.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void Given_An_Archetype_Page_Url_With_No_FurtherResultsUrl_Should_Not_Throw_NullException()
        {
            // Arrange
            var archetypePageUrl = "http://yugioh.wikia.com/wiki/List_of_%22Dizzy%22_cards";
            var document = new HtmlWebPage().Load(archetypePageUrl);
            // Act
            Action act = () => _sut.GetFurtherResultsUrl(document);

            // Assert
            act.ShouldNotThrow<NullReferenceException>();
        }

        [TestCase("http://yugioh.wikia.com/wiki/Special:Ask/-5B-5BCard-20type::Monster-20Card-5D-5D-20-5B-5BArchseries::Blackwing-5D-5D/-3FJapanese-20name/-3FPrimary-20type/-3FSecondary-20type/-3FAttribute%3D-5B-5BAttribute-5D-5D/-3FType%3D-5B-5BType-5D-5D/-3FStars-20string%3D-5B-5BLevel-5D-5D-2F-20-5B-5BRank-5D-5D/-3FATK-20string%3D-5B-5BATK-5D-5D/-3FDEF-20string%3D-5B-5BDEF-5D-5D/mainlabel%3D/limit%3D50/offset%3D50/format%3Dtable/headers%3D-20plain/searchlabel%3D-20...-20further-20results-20(62-20more)/class%3D-20sortable-20wikitable-20smwtable-20card-2Dlist", "Blackwing - Gofu the Vague Shadow")]
        [TestCase("http://yugioh.wikia.com/wiki/Special:Ask?x=-5B-5BClass-201%3A%3AOfficial-5D-5D-20-5B-5BCard-20type%3A%3AMonster-20Card-5D-5D-20-5B-5BArchseries%3A%3AHERO-5D-5D%2F-3FEnglish-20name-20%28linked%29%3D%2F-3FJapanese-20name%2F-3FPrimary-20type%2F-3FSecondary-20type%2F-3FAttribute%3D-5B-5BAttribute-5D-5D%2F-3FType%3D-5B-5BType-5D-5D%2F-3FStars-20string%3D-5B-5BLevel-5D-5D-2F-20-5B-5BRank-5D-5D%2F-3FATK-20string%3D-5B-5BATK-5D-5D%2F-3FDEF-20string%3D-5B-5BDEF-5D-5D&amp;mainlabel=%20-&amp;limit=50&amp;offset=50&amp;format=table&amp;headers=%20plain&amp;searchlabel=%20...%20further%20results%20%2864%20more%29&amp;class=%20sortable%20wikitable%20smwtable%20card-list", "Destiny HERO - Dasher")]
        public void Given_An_Archetype_Page_Url_Should_Return_Card_From_FurtherResultsUrl(string archetypeUrl, string expected)
        {
            // Arrange
            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");

            // Act
            var result = _sut.CardsFromFurtherResultsUrl(archetypeUrl);

            // Assert
            result.Should().Contain(cn => cn.Equals(expected, StringComparison.OrdinalIgnoreCase));
        }

        [TestCase("http://yugioh.wikia.com/wiki/List_of_%22Blackwing%22_cards", "Blackwing - Gust the Backblast")]
        [TestCase("http://yugioh.wikia.com/wiki/List_of_%22HERO%22_cards", "Elemental HERO Grand Neos")]
        public void Given_An_Archetype_Page_Url_Should_Return_List_Containing_Card(string archetypeUrl, string expected)
        {
            // Arrange
            _config.WikiaDomainUrl.Returns("http://yugioh.wikia.com");

            // Act
            var result = _sut.Cards(archetypeUrl);

            // Assert
            result.Should().Contain(cn => cn.Equals(expected, StringComparison.OrdinalIgnoreCase));
        }

    }
}