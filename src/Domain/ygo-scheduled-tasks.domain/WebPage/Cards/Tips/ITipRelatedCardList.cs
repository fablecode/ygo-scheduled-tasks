using System.Collections.Generic;
using HtmlAgilityPack;

namespace ygo_scheduled_tasks.domain.WebPage.Cards.Tips
{
    public interface ITipRelatedCardList
    {
        List<string> ExtractCardsFromTable(HtmlNode table);
    }
}