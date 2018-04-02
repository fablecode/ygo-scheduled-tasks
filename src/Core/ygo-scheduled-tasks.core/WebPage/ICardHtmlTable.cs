using System.Collections.Generic;
using HtmlAgilityPack;

namespace ygo_scheduled_tasks.core.WebPage
{
    public interface ICardHtmlTable
    {
        string GetValue(string key, HtmlNode htmlTable);
        Dictionary<string, string> ProfileData(HtmlNode htmlTable);
        string GetValue(string[] keys, HtmlNode htmlTable);
    }
}