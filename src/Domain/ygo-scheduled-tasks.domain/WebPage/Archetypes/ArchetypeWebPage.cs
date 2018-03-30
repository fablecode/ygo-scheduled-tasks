using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wikia.Api;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.Helpers;

namespace ygo_scheduled_tasks.domain.WebPage.Archetypes
{
    public class ArchetypeWebPage : IArchetypeWebPage
    {
        private readonly IConfig _config;
        private readonly IHtmlWebPage _htmlWebPage;
        private readonly IWikiArticle _wikiArticle;

        public ArchetypeWebPage(IConfig config, IHtmlWebPage htmlWebPage, IWikiArticle wikiArticle)
        {
            _config = config;
            _htmlWebPage = htmlWebPage;
            _wikiArticle = wikiArticle;
        }

        public IEnumerable<string> Cards(Uri archetypeUrl)
        {
            var cardList = new List<string>();

            var archetypeWebPage = _htmlWebPage.Load(archetypeUrl);

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

        public async Task<string> ArchetypeThumbnail(int articleId, string url)
        {
            var profileDetailsList = await _wikiArticle.Details(articleId);
            var profileDetails = profileDetailsList.Items.First();

            var thumbNail = profileDetails.Value.Thumbnail;

            if (string.IsNullOrWhiteSpace(thumbNail))
            {
                var archetypeWebPage = _htmlWebPage.Load(_config.WikiaDomainUrl + url);

                var srcElement = archetypeWebPage.DocumentNode.SelectSingleNode("//img[@class='pi-image-thumbnail']");

                var srcAttribute = srcElement?.Attributes?["src"].Value;

                if(srcAttribute != null)
                    thumbNail = ArchetypeHelper.ExtractThumbnailUrl(srcAttribute);
            }
            else
            {
                thumbNail = ArchetypeHelper.ExtractThumbnailUrl(thumbNail);
            }

            return thumbNail;
        }
    }
}