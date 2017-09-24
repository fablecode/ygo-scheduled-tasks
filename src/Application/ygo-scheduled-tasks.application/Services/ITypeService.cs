using System.Collections.Generic;
using ygo_scheduled_tasks.application.Dto;

namespace ygo_scheduled_tasks.application.Services
{
    public interface ITypeService
    {
        ICollection<TypeDto> GetAll();
    }
}