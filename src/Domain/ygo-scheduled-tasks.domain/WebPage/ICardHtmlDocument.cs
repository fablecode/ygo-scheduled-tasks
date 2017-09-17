using System;
using HtmlAgilityPack;

namespace ygo_scheduled_tasks.domain.WebPage
{
    public interface ICardHtmlDocument
    {
        void Load(string url);
        void Load(Uri url);
        HtmlNode ProfileElement();
        string ProfileImageUrl();
        string ProfileCardDescription();
    }
}