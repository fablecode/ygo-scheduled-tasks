using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class CardRulingService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public CardRulingService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Task<List<RulingSection>> Update(long cardId, List<CardRulingSection> rulingSections)
        {
            var command = new UpdateRulingsCommand
            {
                CardId = cardId,
                Rulings = rulingSections
            };

            return _restClient.Put<UpdateRulingsCommand, List<RulingSection>>($"{_config.ApiUrl}/api/cards/{cardId}/rulings", command);
        }
    }
}