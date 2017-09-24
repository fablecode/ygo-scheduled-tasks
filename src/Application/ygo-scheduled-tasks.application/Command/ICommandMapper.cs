using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.application.Command
{
    public interface ICommandMapper
    {
        AddCardCommand MapToAddCommand(YugiohCard yugiohCard);
        UpdateCardCommand MapToUpdateCommand(YugiohCard yugiohCard);
    }
}