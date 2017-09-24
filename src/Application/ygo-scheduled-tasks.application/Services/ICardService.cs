using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Command;
using ygo_scheduled_tasks.application.Dto;

namespace ygo_scheduled_tasks.application.Services
{
    public interface ICardService
    {
        CardDto CardByName(string name);
        Task<CardDto> Add(AddCardCommand command);
        Task<CardDto> Update(UpdateCardCommand command);
    }
}