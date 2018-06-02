using HtmlAgilityPack;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;

namespace ygo_scheduled_tasks.domain.WebPage.Cards.Tips
{
    public interface ITipRelatedWebPage
    {
        void GetTipRelatedCards(CardTipSection section, UnexpandedArticle item);
        void GetTipRelatedCards(CardTipSection section, string tipRelatedCardListUrl, HtmlNode tipRelatedCardListTable);
    }
}