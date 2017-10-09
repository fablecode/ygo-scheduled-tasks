using System.Collections.Generic;
using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.core.WebPage
{
    public interface ISemanticSearch
    {
        List<SemanticCard> CardsByUrl(string url);
    }
}