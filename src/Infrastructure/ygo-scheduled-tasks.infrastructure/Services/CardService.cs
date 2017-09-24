using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Command;
using ygo_scheduled_tasks.application.Dto;
using ygo_scheduled_tasks.application.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class CardService : ICardService
    {
        public CardDto CardByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<CardDto> Add(AddCardCommand command)
        {
            throw new System.NotImplementedException();
        }

        public Task<CardDto> Update(UpdateCardCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}