using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using ygo_scheduled_tasks.core.WebPage;

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

            Load(new Uri(url));
        }

        public void Load(Uri url)
        {
            _cardPage = _htmlWebPage.Load(url.AbsoluteUri);
        }

        public HtmlNode ProfileElement()
        {
            return _cardPage.DocumentNode.SelectSingleNode("//div[@id='WikiaArticle']//table[contains(@class, 'cardtable')]");
        }

        public IDictionary<string, string> ProfileData(HtmlNode htmlTable)
        {
            var response = new Dictionary<string, string>();

            var htmlTableRows = htmlTable.SelectNodes("./tr");

            if (htmlTableRows != null && htmlTableRows.Any())
            {
                foreach (var row in htmlTableRows)
                {
                    var key = row.SelectSingleNode("./th[contains(@class, 'cardtablerowheader')]");
                    var value = row.SelectSingleNode("./td[contains(@class, 'cardtablerowdata')]");

                    if (key != null && value != null && !response.ContainsKey(key.InnerText))
                    {
                        var cardEffectTypes = key.InnerText == "Card effect types" ? string.Join(",", value.SelectNodes("./ul/li").Select(t => t.InnerText.Trim())) : value.InnerText;

                        response.Add(key.InnerText.Trim(), cardEffectTypes);
                    }
                }
            }

            return response;
        }

        public string ProfileImageUrl()
        {
            var imageUrl = _cardPage.DocumentNode.SelectSingleNode("//td[@class='cardtable-cardimage']/a/img").Attributes["src"].Value;

            if (imageUrl.Contains("revision"))
                imageUrl = imageUrl.Substring(0, imageUrl.IndexOf("/revision", StringComparison.Ordinal));

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