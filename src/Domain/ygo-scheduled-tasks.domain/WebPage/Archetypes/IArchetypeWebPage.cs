using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ygo_scheduled_tasks.domain.WebPage.Archetypes
{
    public interface IArchetypeWebPage
    {
        IEnumerable<string> Cards(Uri archetypeUrl);
        string GetFurtherResultsUrl(HtmlDocument archetypeWebPage);
        List<string> CardsFromFurtherResultsUrl(string furtherResultsUrl);
        Task<string> ArchetypeThumbnail(int articleId, string url);
    }
}