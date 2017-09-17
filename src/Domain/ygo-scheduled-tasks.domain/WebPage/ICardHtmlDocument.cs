using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace ygo_scheduled_tasks.domain.WebPage
{
    public interface ICardHtmlDocument
    {
        void Load(string url);
        void Load(Uri url);
        HtmlNode ProfileElement();
        IDictionary<string, string> ProfileData(HtmlNode htmlTable);
        string ProfileImageUrl();
        string ProfileCardDescription();
    }
}