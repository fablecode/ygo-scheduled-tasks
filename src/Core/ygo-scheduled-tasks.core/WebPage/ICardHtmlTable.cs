using HtmlAgilityPack;

namespace ygo_scheduled_tasks.core.WebPage
{
    public interface ICardHtmlTable
    {
        void Load(HtmlNode htmlTable);
        long? GetLongValue(string key);
        int? GetIntValue(string key);
        string GetValue(params string[] keys);
        string GetCardAttribute();
    }
}