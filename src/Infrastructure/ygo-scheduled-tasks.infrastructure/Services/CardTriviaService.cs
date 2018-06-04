using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class CardTriviaService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public CardTriviaService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Task<List<TriviaSection>> Update(long cardId, List<CardTriviaSection> rulingSections)
        {
            var command = new UpdateTriviaCommand
            {
                CardId = cardId,
                Trivia = rulingSections
            };

            return _restClient.Put<UpdateTriviaCommand, List<TriviaSection>>($"{_config.ApiUrl}/api/cards/{cardId}/trivia", command);
        }
    }
}