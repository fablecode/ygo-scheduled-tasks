using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace ygo_scheduled_tasks.domain.WebPage.Cards.Tips
{
    public class TipRelatedCardList : ITipRelatedCardList
    {
        private readonly IConfig _config;

        public TipRelatedCardList(IConfig config)
        {
            _config = config;

        }

        public List<string> ExtractCardsFromTable(HtmlNode table)
        {
            var cardNameList = table.SelectNodes("//tr/td[position() = 1]/a");

            return cardNameList.Select(cn => cn.InnerText).ToList();
        }
    }
}