using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Dto;
using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.application.Command
{
    public interface ICommandMapper
    {
        Task<AddCardCommand> MapToAddCommand(YugiohCard yugiohCard);
        Task<UpdateCardCommand> MapToUpdateCommand(YugiohCard yugiohCard, CardDto card);
    }
}