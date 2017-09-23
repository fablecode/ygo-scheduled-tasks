using ygo_scheduled_tasks.application.Dto;

namespace ygo_scheduled_tasks.application.Services
{
    public interface ICardService
    {
        CardDto CardByName(string name);
    }
}