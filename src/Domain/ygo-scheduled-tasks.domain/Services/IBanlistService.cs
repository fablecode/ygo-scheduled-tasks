using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface IBanlistService
    {
        Task<Banlist> BanlistById(long id);
        Task<Banlist> Add(AddBanlistCommand command);
        Task<Banlist> Update(UpdateBanlistCommand command);
    }

    public class UpdateBanlistCommand
    {
    }

    public class AddBanlistCommand
    {
    }
}