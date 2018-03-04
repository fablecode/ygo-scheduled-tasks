using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class ArchetypeService : IArchetypeService
    {
        public Task<Archetype> AddOrUpdate(YugiohArchetype archetypeToAddOrUpdated)
        {
            throw new System.NotImplementedException();
        }
    }
}