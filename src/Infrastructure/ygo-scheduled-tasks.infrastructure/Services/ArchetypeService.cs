using System;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class ArchetypeService : IArchetypeService
    {
        public Task<Card> ArchetypeByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Archetype> Add(YugiohArchetype archetype)
        {
            throw new NotImplementedException();
        }

        public Task<Archetype> Update(YugiohArchetype archetype)
        {
            throw new NotImplementedException();
        }
    }
}