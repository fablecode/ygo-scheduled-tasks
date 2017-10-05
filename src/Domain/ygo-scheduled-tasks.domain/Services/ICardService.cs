using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.Command;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface ICardService
    {
        Task<Card> CardById(long id);
        Task<Card> CardByName(string name);
        Task<Card> Add(AddCardCommand command);
        Task<Card> Update(UpdateCardCommand command);
    }
}