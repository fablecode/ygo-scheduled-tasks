using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.Command;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface IArchetypeCardsService
    {
        Task<IEnumerable<ArchetypeCard>> Update(UpdateArchetypeCardsCommand updateComman);
    }
}