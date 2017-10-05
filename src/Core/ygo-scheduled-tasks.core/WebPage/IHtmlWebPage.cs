using HtmlAgilityPack;

namespace ygo_scheduled_tasks.core.WebPage
{
    public interface IHtmlWebPage
    {
        HtmlDocument Load(string webPageUrl);
    }
}