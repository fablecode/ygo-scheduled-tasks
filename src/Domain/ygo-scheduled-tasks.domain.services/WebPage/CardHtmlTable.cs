using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HtmlAgilityPack;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.domain.services.WebPage
{
    public class CardHtmlTable : ICardHtmlTable
    {
        public const string Name = "English";
        public const string Number = "Passcode";
        public const string CardType = "Card type";
        public const string Property = "Property";
        public const string Attribute = "Attribute";
        public const string Level = "Level";
        public const string Rank = "Rank";
        public const string LinkArrows = "Link Arrows";
        public const string Materials = "Materials";
        public const string CardEffectTypes = "Card effect types";
        public const string PendulumScale = "Pendulum Scale";
        public static readonly string[] AtkAndDef = { "ATK / DEF", "ATK/DEF" };
        public static readonly string[] AtkAndLink = { "ATK / LINK", "ATK/LINK" };
        public static readonly string[] Types = { "Type", "Types"} ;

        private Dictionary<string, string> _cardProfileLookup;

        public void Load(HtmlNode htmlTable)
        {
            _cardProfileLookup = new Dictionary<string, string>();

            if (htmlTable != null)
            {
                var htmlTableRows = htmlTable.SelectNodes("./tr");

                if (htmlTableRows != null && htmlTableRows.Any())
                {
                    foreach (var row in htmlTableRows)
                    {
                        var key = row.SelectSingleNode("./th[contains(@class, 'cardtablerowheader')]");
                        var value = row.SelectSingleNode("./td[contains(@class, 'cardtablerowdata')]");

                        if (key != null && value != null && !_cardProfileLookup.ContainsKey(key.InnerText))
                        {
                            var cardEffectTypes = key.InnerText == "Card effect types" ? string.Join(",", value.SelectNodes("./ul/li").Select(t => t.InnerText.Trim())) : value.InnerText;

                            _cardProfileLookup.Add(key.InnerText.Trim(), cardEffectTypes);
                        }
                    }
                }
            }
        }

        public long? GetLongValue(string key)
        {
            return !_cardProfileLookup.TryGetValue(key, out var value) ? 0 : long.Parse(value);
        }

        public int? GetIntValue(string key)
        {
            return !_cardProfileLookup.TryGetValue(key, out var value) ? null : (int?)int.Parse(value);
        }

        public string GetValue(params string[] keys)
        {
            string value = string.Empty;

            if (_cardProfileLookup == null || keys == null)
                return value;

            foreach (var key in keys)
            {
                var isKeyFound = _cardProfileLookup.TryGetValue(key, out value);

                if (isKeyFound)
                {
                    value = value.Trim().Replace("\n", null);
                    break;
                }
            }

            return value;
        }

        public string GetCardAttribute()
        {
            if (!_cardProfileLookup.TryGetValue(Attribute, out var attribute))
                return string.Empty;

            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(attribute.Trim().ToLower());
        }
    }
}