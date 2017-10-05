using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface ITypeService
    {
        Task<ICollection<Type>> GetAll();
    }
}