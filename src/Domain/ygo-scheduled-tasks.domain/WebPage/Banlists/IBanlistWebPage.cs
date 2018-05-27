using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using ygo_scheduled_tasks.core.Enums;

namespace ygo_scheduled_tasks.domain.WebPage.Banlists
{
    public interface IBanlistWebPage
    {
        Dictionary<string, List<Uri>> GetBanlistUrlList(BanlistType banlistType, string banlistUrl);
        Dictionary<string, List<Uri>> GetBanlistUrlList(HtmlNode banlistUrlListNode);
    }
}