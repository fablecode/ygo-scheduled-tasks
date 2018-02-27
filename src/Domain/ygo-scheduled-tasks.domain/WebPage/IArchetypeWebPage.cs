using System.Collections.Generic;
using HtmlAgilityPack;

namespace ygo_scheduled_tasks.domain.WebPage
{
    public interface IArchetypeWebPage
    {
        IEnumerable<string> Cards(string archetypeUrl);
        string GetFurtherResultsUrl(HtmlDocument archetypeWebPage);
        List<string> CardsFromFurtherResultsUrl(string furtherResultsUrl);
    }
}