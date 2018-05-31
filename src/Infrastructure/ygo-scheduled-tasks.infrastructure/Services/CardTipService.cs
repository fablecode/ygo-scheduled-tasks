using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class CardTipService : ICardTipService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public CardTipService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Task<List<TipSection>> Update(long cardId, List<CardTipSection> tipSections)
        {
            var command = new UpdateTipsCommand
            {
                CardId = cardId,
                Tips = tipSections
            };

            return _restClient.Put<UpdateTipsCommand, List<TipSection>>($"{_config.ApiUrl}/api/cards/{cardId}/tips", command);
        }
    }
}