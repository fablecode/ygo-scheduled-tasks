using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Dto;
using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.application.Services
{
    public interface IYugiohCardService
    {
        Task<CardDto> AddOrUpdate(YugiohCard yugiohCard);
    }
}