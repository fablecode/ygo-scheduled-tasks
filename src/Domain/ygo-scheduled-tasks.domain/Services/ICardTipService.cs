using System.Collections.Generic;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface ICardTipService
    {
        void Update(long cardId, List<CardTipSection> tipSections);
    }
}