using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface IYugiohBanlistService
    {
        Banlist AddOrUpdate(YugiohBanlist banlist);
    }
}