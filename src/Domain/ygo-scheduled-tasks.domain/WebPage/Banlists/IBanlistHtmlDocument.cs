using HtmlAgilityPack;
using ygo_scheduled_tasks.core.Enums;

namespace ygo_scheduled_tasks.domain.WebPage.Banlists
{
    public interface IBanlistHtmlDocument
    {
        HtmlNode GetBanlistHtmlNode(BanlistType banlistType, string banlistUrl);
        HtmlNode GetBanlistHtmlNode(BanlistType banlistType, HtmlDocument document);
    }
}