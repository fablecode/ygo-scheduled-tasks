using HtmlAgilityPack;
using ygo_scheduled_tasks.core.Enums;
using ygo_scheduled_tasks.core.WebPage;

namespace ygo_scheduled_tasks.domain.WebPage.Banlists
{
    public class BanlistHtmlDocument : IBanlistHtmlDocument
    {
        private readonly IHtmlWebPage _htmlWebPage;

        public BanlistHtmlDocument(IHtmlWebPage htmlWebPage)
        {
            _htmlWebPage = htmlWebPage;
        }

        public HtmlNode GetBanlistHtmlNode(BanlistType banlistType, string banlistUrl)
        {
            return GetBanlistHtmlNode(banlistType, _htmlWebPage.Load(banlistUrl));
        }
        public HtmlNode GetBanlistHtmlNode(BanlistType banlistType, HtmlDocument document)
        {
            return document
                .DocumentNode
                .SelectSingleNode($"//*[contains(@class,'nowraplinks navbox-subgroup')]/tr/th/i[contains(text(), '{banlistType.ToString().ToUpper()}')]")
                .ParentNode
                .ParentNode
                .SelectSingleNode("./td/table/tr[1]/td[1]/div/ul");
        }
    }
}