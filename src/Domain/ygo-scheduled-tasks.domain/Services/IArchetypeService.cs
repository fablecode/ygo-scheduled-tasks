using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.Command;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface IArchetypeService
    {
        Task<Archetype> ArchetypeById(long id);
        Task<Archetype> ArchetypeByName(string name);
        Task<Archetype> Add(AddArchetypeCommand command);
        Task<Archetype> Update(UpdateArchetypeCommand command);
    }
}