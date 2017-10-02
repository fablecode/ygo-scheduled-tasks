using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Dto;

namespace ygo_scheduled_tasks.application.Api
{
    public interface IAttributeService
    {
        Task<ICollection<AttributeDto>> GetAll();
    }
}