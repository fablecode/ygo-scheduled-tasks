using HtmlAgilityPack;

namespace ygo_scheduled_tasks.domain.WebPage.Cards.Tips
{
    public interface ITipRelatedHtmlDocument
    {
        HtmlNode GetTable(HtmlDocument document);
        string GetUrl(HtmlDocument document);
    }
}