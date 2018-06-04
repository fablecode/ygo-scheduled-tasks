using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;

namespace ygo_scheduled_tasks.domain
{
    public interface ICardRulingService
    {
        Task<List<RulingSection>> Update(long cardId, List<CardRulingSection> rulingSections);
    }
}