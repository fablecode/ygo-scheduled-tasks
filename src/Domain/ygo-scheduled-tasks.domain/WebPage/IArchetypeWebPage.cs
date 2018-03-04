using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace ygo_scheduled_tasks.domain.WebPage
{
    public interface IArchetypeWebPage
    {
        IEnumerable<string> Cards(Uri archetypeUrl);
        string GetFurtherResultsUrl(HtmlDocument archetypeWebPage);
        List<string> CardsFromFurtherResultsUrl(string furtherResultsUrl);
    }
}