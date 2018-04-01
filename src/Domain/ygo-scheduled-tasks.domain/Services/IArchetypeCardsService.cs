using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface IArchetypeCardsService
    {
        Task<IEnumerable<ArchetypeCard>> Update(long archetypeId, IEnumerable<string> cards);
    }
}