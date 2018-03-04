using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using ygo_scheduled_tasks.core.WebPage;

namespace ygo_scheduled_tasks.domain.WebPage.Archetypes
{
    public class ArchetypeWebPage : IArchetypeWebPage
    {
        private readonly IConfig _config;
        private readonly IHtmlWebPage _htmlWebPage;

        public ArchetypeWebPage(IConfig config, IHtmlWebPage htmlWebPage)
        {
            _config = config;
            _htmlWebPage = htmlWebPage;
        }

        public IEnumerable<string> Cards(string archetypeUrl)
        {
            var cardList = new List<string>();

            var archetypeWebPage = _htmlWebPage.Load(_config.WikiaDomainUrl + archetypeUrl);

            var tableCollection = archetypeWebPage.DocumentNode
                .SelectNodes("//table")
                .Where(t => t.Attributes["class"] != null && t.Attributes["class"].Value.Contains("card-list"))
                .ToList();

            foreach (var tb in tableCollection)
            {
                var cardLinks = tb.SelectNodes("./tr/td[position() = 1]/a");

                cardList.AddRange(cardLinks.Select(cn => cn.InnerText));
            }

            var furtherResultsUrl = GetFurtherResultsUrl(archetypeWebPage);

            if (!string.IsNullOrEmpty(furtherResultsUrl))
            {
                if (!furtherResultsUrl.Contains("http"))
                    furtherResultsUrl = _config.WikiaDomainUrl + furtherResultsUrl;

                cardList = cardList.Union(CardsFromFurtherResultsUrl(furtherResultsUrl)).ToList();
            }

            return cardList;
        }

        public List<string> CardsFromFurtherResultsUrl(string furtherResultsUrl)
        {
            var cardList = new List<string>();

            // change result set to 500
            var newUrl = furtherResultsUrl.Replace("limit%3D50", "limit%3D500");

            // sematic search page
            var sematicSearchPage = _htmlWebPage.Load(newUrl);

            var cardNameList =
                sematicSearchPage.DocumentNode.SelectNodes(
                    "//*[@id='mw-content-text']/table/tbody/tr/td[1]/a");

            if (cardNameList != null)
                cardList.AddRange(cardNameList.Select(cn => cn.InnerText ));

            return cardList.ToList();
        }

        public string GetFurtherResultsUrl(HtmlDocument archetypeWebPage)
        {
            return archetypeWebPage.DocumentNode.SelectSingleNode("//span[@class='smw-table-furtherresults']/a")?.Attributes["href"].Value;
        }
    }
}