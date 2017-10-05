using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.domain.Command
{
    public interface ICommandMapper
    {
        Task<AddCardCommand> MapToAddCommand(YugiohCard yugiohCard);
        Task<UpdateCardCommand> MapToUpdateCommand(YugiohCard yugiohCard, Card card);
    }
}