using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Dto;

namespace ygo_scheduled_tasks.application.Services
{
    public interface ISubCategoryService
    {
        Task<ICollection<SubCategoryDto>> GetAll();
    }
}