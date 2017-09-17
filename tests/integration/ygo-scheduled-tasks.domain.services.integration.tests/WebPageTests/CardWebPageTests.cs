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
            var sut = new CardWebPage(new CardHtmlDocument(new HtmlWebPage()), new CardHtmlTable());
        }
    }

    public class CardWebPage
    {
        private readonly ICardHtmlDocument _cardHtmlDocument;
        private readonly ICardHtmlTable _cardHtmlTable;
        private HtmlDocument _cardPage;

        public CardWebPage(ICardHtmlDocument cardHtmlDocument, ICardHtmlTable cardHtmlTable)
        {
            _cardHtmlDocument = cardHtmlDocument;
            _cardHtmlTable = cardHtmlTable;
        }

        public void Load(string url)
        {
            Load(new Uri(url));
        }

        public void Load(Uri url)
        {
            _cardHtmlDocument.Load(url);
            _cardHtmlTable.Load(_cardHtmlDocument.ProfileElement());
        }
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