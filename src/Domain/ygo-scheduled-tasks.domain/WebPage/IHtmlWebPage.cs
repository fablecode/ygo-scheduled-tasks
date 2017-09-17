using HtmlAgilityPack;

namespace ygo_scheduled_tasks.domain.WebPage
{
    public interface IHtmlWebPage
    {
        HtmlDocument Load(string webPageUrl);
    }
}