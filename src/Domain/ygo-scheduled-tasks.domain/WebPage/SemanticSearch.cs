using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.core.WebPage;

namespace ygo_scheduled_tasks.domain.WebPage
{
    public class SemanticSearch : ISemanticSearch
    {
        private readonly IConfig _config;
        private readonly IHtmlWebPage _htmlWebPage;

        public SemanticSearch(IConfig config, IHtmlWebPage htmlWebPage)
        {
            _config = config;
            _htmlWebPage = htmlWebPage;
        }

        public List<SemanticCard> CardsByUrl(string url)
        {
            HtmlNode nextLink;
            var semanticCardList = new List<SemanticCard>();

            do
            {
                var doc = _htmlWebPage.Load(url);

                var tableRows = doc.DocumentNode.SelectNodes("//table[@class='sortable wikitable smwtable']/tbody/tr") ?? doc.DocumentNode.SelectNodes("//table[@class='sortable wikitable smwtable card-list']/tbody/tr");

                foreach (var row in tableRows)
                {
                    var semanticCard = new SemanticCard
                    {
                        Name = row.SelectSingleNode("td[position() = 1]")?.InnerText.Trim(),
                        Url = row.SelectSingleNode("td[position() = 1]/a")?.Attributes["href"]?.Value,
                    };

                    if(!string.IsNullOrWhiteSpace(semanticCard.Name))
                        semanticCardList.Add(semanticCard);
                }

                nextLink = doc.DocumentNode.SelectSingleNode("//a[contains(text(), 'Next')]");

                if (nextLink != null)
                {
                    var hrefLink = $"{_config.WikiaDomainUrl}{nextLink.Attributes["href"].Value}";

                    hrefLink = WebUtility.HtmlDecode(hrefLink);

                    url = hrefLink;
                }

            } while (nextLink != null);

            return semanticCardList;
        }
    }
}