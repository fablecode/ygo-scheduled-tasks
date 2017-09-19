using System;
using ygo_scheduled_tasks.domain.Model;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.domain.services.WebPage
{
    public class CardWebPage : ICardWebPage
    {
        private readonly ICardHtmlDocument _cardHtmlDocument;
        private readonly ICardHtmlTable _cardHtmlTable;

        public CardWebPage(ICardHtmlDocument cardHtmlDocument, ICardHtmlTable cardHtmlTable)
        {
            _cardHtmlDocument = cardHtmlDocument;
            _cardHtmlTable = cardHtmlTable;
        }


        public YugiohCard GetYugiohCard(string url)
        {
            return GetYugiohCard(new Uri(url));
        }

        public YugiohCard GetYugiohCard(Uri url)
        {
            Load(url);
            return GetYugiohCard();
        }


        #region private helpers

        private void Load(Uri url)
        {
            _cardHtmlDocument.Load(url);
            _cardHtmlTable.Load(_cardHtmlDocument.ProfileElement());
        }

        private YugiohCard GetYugiohCard()
        {
            var response = new YugiohCard();

            response.ImageUrl = _cardHtmlDocument.ProfileImageUrl();
            response.Name = _cardHtmlTable.GetValue(CardHtmlTable.Name);
            response.Description = _cardHtmlDocument.ProfileCardDescription();
            response.CardNumber = _cardHtmlTable.GetValue(CardHtmlTable.Number);
            response.CardType = _cardHtmlTable.GetValue(CardHtmlTable.CardType);
            response.Property = _cardHtmlTable.GetValue(CardHtmlTable.Property);
            response.Attribute = _cardHtmlTable.GetCardAttribute();
            response.Level = _cardHtmlTable.GetIntValue(CardHtmlTable.Level);
            response.Rank = _cardHtmlTable.GetIntValue(CardHtmlTable.Rank);
            response.AtkDef = _cardHtmlTable.GetValue(CardHtmlTable.AtkAndDef);
            response.AtkLink = _cardHtmlTable.GetValue(CardHtmlTable.AtkAndLink);
            response.Types = _cardHtmlTable.GetValue(CardHtmlTable.Types);
            response.Materials = _cardHtmlTable.GetValue(CardHtmlTable.Materials);
            response.CardEffectTypes = _cardHtmlTable.GetValue(CardHtmlTable.CardEffectTypes);
            response.PendulumScale = _cardHtmlTable.GetIntValue(CardHtmlTable.PendulumScale);

            return response;
        } 

        #endregion
    }
}