using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface IArchetypeService
    {
        Task<Archetype> ArchetypeByName(string name);
        Task<Archetype> Add(YugiohArchetype archetype);
        Task<Archetype> Update(YugiohArchetype archetype);
    }
}