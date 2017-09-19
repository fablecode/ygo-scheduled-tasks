using System;
using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.domain.WebPage
{
    public interface ICardWebPage
    {
        YugiohCard GetYugiohCard(string url);

        YugiohCard GetYugiohCard(Uri url);
    }
}