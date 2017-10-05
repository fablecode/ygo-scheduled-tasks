using System;
using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.core.WebPage
{
    public interface ICardWebPage
    {
        YugiohCard GetYugiohCard(string url);

        YugiohCard GetYugiohCard(Uri url);
    }
}