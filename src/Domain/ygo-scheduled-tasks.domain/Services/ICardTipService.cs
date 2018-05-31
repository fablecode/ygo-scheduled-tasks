using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface ICardTipService
    {
        Task<List<TipSection>> Update(long cardId, List<CardTipSection> tipSections);
    }
}