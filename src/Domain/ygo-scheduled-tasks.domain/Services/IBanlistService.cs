using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.Command;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface IBanlistService
    {
        Task<Banlist> BanlistById(long id);
        Task<Banlist> Add(AddBanlistCommand command);
        Task<Banlist> Update(UpdateBanlistCommand command);
        Task<IList<BanlistCard>> Update(long id, UpdateBanlistCardsCommand command);
    }
}