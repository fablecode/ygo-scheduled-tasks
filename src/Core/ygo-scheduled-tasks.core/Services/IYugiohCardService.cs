using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.core.Services
{
    public interface IYugiohCardService
    {
        Task<Card> AddOrUpdate(YugiohCard yugiohCard);
    }
}