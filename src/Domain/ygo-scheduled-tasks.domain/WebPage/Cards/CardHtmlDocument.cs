using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.Helpers;

namespace ygo_scheduled_tasks.domain.WebPage.Cards
{
    public class CardHtmlDocument : ICardHtmlDocument
    {
        private readonly IHtmlWebPage _htmlWebPage;
        private HtmlDocument _cardPage;

        public CardHtmlDocument(IHtmlWebPage htmlWebPage)
        {
            _htmlWebPage = htmlWebPage;
        }

        public void Load(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(nameof(url));

            _cardPage = _htmlWebPage.Load(url);
        }

        public void Load(Uri url)
        {
            _cardPage = _htmlWebPage.Load(url);
        }

        public HtmlNode ProfileElement()
        {
            return _cardPage.DocumentNode.SelectSingleNode("//div[@id='WikiaArticle']//table[contains(@class, 'cardtable')]");
        }

        public string ProfileImageUrl()
        {
            var imageUrl = _cardPage.DocumentNode.SelectSingleNode("//td[@class='cardtable-cardimage']/a/img").Attributes["src"].Value;

            if (imageUrl.Contains("revision"))
                imageUrl = ImageHelper.ExtractImageUrl(imageUrl);

            return imageUrl;
        }

        public string ProfileCardDescription()
        {
            var pattern = @"(?!</?br>)<.*?>";
            var descNode = _cardPage.DocumentNode.SelectSingleNode("//b[text()[contains(., 'Card descriptions')]]/../table[1]/tr[1]/td/table/tr[3]/td")?.InnerHtml;

            if (descNode != null)
            {
                descNode = Regex.Replace(descNode, pattern, string.Empty, RegexOptions.Multiline);

                return descNode.Trim();
            }

            return string.Empty;
        }
    }
}