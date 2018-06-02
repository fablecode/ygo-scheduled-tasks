using HtmlAgilityPack;

namespace ygo_scheduled_tasks.domain.WebPage.Cards.Tips
{
    public class TipRelatedHtmlDocument : ITipRelatedHtmlDocument
    {
        private readonly IConfig _config;

        public TipRelatedHtmlDocument(IConfig config)
        {
            _config = config;
        }

        public HtmlNode GetTable(HtmlDocument document)
        {
            return document.DocumentNode.SelectSingleNode("//table[@class='sortable wikitable smwtable']");
        }

        public string GetUrl(HtmlDocument document)
        {
            var furtherResultsUrl =  document.DocumentNode.SelectSingleNode("//span[@class='smw-table-furtherresults']/a")?.Attributes["href"]?.Value;

            if (!string.IsNullOrWhiteSpace(furtherResultsUrl) && !furtherResultsUrl.Contains("http://"))
                furtherResultsUrl = _config.WikiaDomainUrl + furtherResultsUrl;

            return furtherResultsUrl;
        }
    }
}