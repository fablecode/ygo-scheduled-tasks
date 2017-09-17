using System.Collections.Generic;
using HtmlAgilityPack;
using NUnit.Framework;
using ygo_scheduled_tasks.domain.services.WebPage;

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
}