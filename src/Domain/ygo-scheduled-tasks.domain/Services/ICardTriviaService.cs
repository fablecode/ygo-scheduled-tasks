using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;

namespace ygo_scheduled_tasks.domain.Services
{
    public interface ICardTriviaService
    {
        Task<List<TriviaSection>> Update(long cardId, List<CardTriviaSection> rulingSections);
    }
}