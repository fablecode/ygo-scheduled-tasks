using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Dto;

namespace ygo_scheduled_tasks.application.Api
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryDto>> GetAll();
    }
}