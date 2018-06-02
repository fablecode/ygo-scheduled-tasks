using System;
using System.Linq;
using HtmlAgilityPack;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;

namespace ygo_scheduled_tasks.domain.WebPage.Cards.Tips
{
    public class TipRelatedWebPage : ITipRelatedWebPage
    {
        private readonly IConfig _config;
        private readonly ITipRelatedCardList _tipRelatedCardList;
        private readonly ITipRelatedHtmlDocument _tipRelatedHtmlDocument;
        private readonly ISemanticSearch _semanticSearch;

        public TipRelatedWebPage
        (
            IConfig config,
            ITipRelatedCardList tipRelatedCardList,
            ITipRelatedHtmlDocument tipRelatedHtmlDocument,
            ISemanticSearch semanticSearch
        )
        {
            _config = config;
            _tipRelatedCardList = tipRelatedCardList;
            _tipRelatedHtmlDocument = tipRelatedHtmlDocument;
            _semanticSearch = semanticSearch;
        }

        public void GetTipRelatedCards(CardTipSection section, UnexpandedArticle item)
        {
            if (section == null)
                throw new ArgumentNullException(nameof(section));

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var tipWebPage = new HtmlWeb().Load(_config.WikiaDomainUrl + item.Url);

            //Get tip related card list url
            var cardListUrl = _tipRelatedHtmlDocument.GetUrl(tipWebPage);

            //get tips related card list table
            var cardListTable = _tipRelatedHtmlDocument.GetTable(tipWebPage);

            GetTipRelatedCards(section, cardListUrl, cardListTable);
        }

        public void GetTipRelatedCards(CardTipSection section, string tipRelatedCardListUrl, HtmlNode tipRelatedCardListTable)
        {
            if(section == null)
                throw new ArgumentNullException(nameof(section));

            if (!string.IsNullOrEmpty(tipRelatedCardListUrl))
            {
                var cardsFromUrl = _semanticSearch.CardsByUrl(tipRelatedCardListUrl);
                section.Tips.AddRange(cardsFromUrl.Select(c => c.Name));
            }
            else if (tipRelatedCardListTable != null)
            {
                var cardsFromTable = _tipRelatedCardList.ExtractCardsFromTable(tipRelatedCardListTable);
                section.Tips.AddRange(cardsFromTable);
            }
        }
    }
}