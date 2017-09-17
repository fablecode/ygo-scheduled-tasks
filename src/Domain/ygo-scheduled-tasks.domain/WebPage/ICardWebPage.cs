using System;
using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.domain.WebPage
{
    public interface ICardWebPage
    {
        void Load(string url);
        void Load(Uri url);
        YugiohCard GetYugiohCard();
    }
}