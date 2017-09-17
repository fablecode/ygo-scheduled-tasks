using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.services.WebPage;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.domain.services.integration.tests.WebPageTests
{
    [TestFixture]
    public class CardWebPageTests
    {
        [SetUp]
        public void Setup()
        {
            var sut = new CardWebPage(new HtmlWebPage());
        }
    }

    public class CardWebPage
    {
        private readonly IHtmlWebPage _htmlWebPage;
        private HtmlDocument _cardPage;

        public CardWebPage(IHtmlWebPage htmlWebPage)
        {
            _htmlWebPage = htmlWebPage;
        }

        public void Load(string url)
        {
            Load(new Uri(url));
        }

        public void Load(Uri url)
        {
            _cardPage = _htmlWebPage.Load(url.AbsoluteUri);
        }
    }

    [TestFixture]
    public class CardWebPageHelperTests
    {
        [SetUp]
        public void Setup()
        {
            
        }
    }

    public static class CardWebPageHelper
    {
        
    }

    public sealed class YugiohCard
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }
        public int Level { get; set; }
        public int Rank { get; set; }
        public long PendulumScale { get; set; }
        public string AtkDef { get; set; }
        public string CardNumber { get; set; }
        public string Materials { get; set; }
        public List<string> CardEffectTypes { get; set; }
        public string Property { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> LinkArrows { get; set; }
    }
}