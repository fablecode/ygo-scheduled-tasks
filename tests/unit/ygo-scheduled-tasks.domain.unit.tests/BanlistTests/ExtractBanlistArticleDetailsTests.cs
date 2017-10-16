﻿using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ygo_scheduled_tasks.core.Enums;
using ygo_scheduled_tasks.domain.Helpers;

namespace ygo_scheduled_tasks.domain.unit.tests.BanlistTests
{
    [TestFixture]
    public class ExtractBanlistArticleDetailsTests
    {
        [TestCase("OCG in effect since September 1, 2007.", BanlistType.Ocg)]
        [TestCase("TCG in effect since January 1, 2016.", BanlistType.Tcg)]
        [TestCase("TCG in effect since April 1, 2010", BanlistType.Tcg)]
        public void Given_A_Banlist_TitleText_Should_Extract_Banlist_Type(string titleText, BanlistType expected)
        {
            // Arrange

            // Act
            var result = BanlistHelpers.ExtractBanlistArticleDetails(Arg.Any<int>(), titleText);

            // Assert
            result.BanlistType.Should().Be(expected);
        }

        [TestCase("OCG in effect since September 1, 2007.", "September 1, 2007")]
        [TestCase("TCG in effect since January 1, 2016.", "January 1, 2016")]
        [TestCase("TCG in effect since April 1, 2010", "April 1, 2010")]
        public void Given_A_Banlist_TitleText_Should_Extract_Banlist_StartDate(string titleText, DateTime expected)
        {
            // Arrange

            // Act
            var result = BanlistHelpers.ExtractBanlistArticleDetails(Arg.Any<int>(), titleText);

            // Assert
            result.StartDate.Should().Be(expected);
        }

        [TestCase(940353, "OCG in effect since September 1, 2007.", 940353)]
        [TestCase(2324242, "TCG in effect since January 1, 2016.", 2324242)]
        [TestCase(6258383, "TCG in effect since April 1, 2010", 6258383)]
        public void Given_A_Banlist_TitleText_And_An_ArticleId_Should_Assign_ArticleId_To_Returned_BanlistArticleSummary(int article, string titleText, int expected)
        {
            // Arrange

            // Act
            var result = BanlistHelpers.ExtractBanlistArticleDetails(article, titleText);

            // Assert
            result.ArticleId.Should().Be(expected);
        }

    }
}