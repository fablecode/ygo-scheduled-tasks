using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Dto;

namespace ygo_scheduled_tasks.application.Services
{
    public interface ITypeService
    {
        Task<ICollection<TypeDto>> GetAll();
    }
}