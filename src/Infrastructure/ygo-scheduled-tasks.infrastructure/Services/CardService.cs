﻿using System.Threading.Tasks;
using ygo_scheduled_tasks.application;
using ygo_scheduled_tasks.application.Client;
using ygo_scheduled_tasks.application.Command;
using ygo_scheduled_tasks.application.Dto;
using ygo_scheduled_tasks.application.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class CardService : ICardService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public CardService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Task<CardDto> CardById(long id)
        {
            return _restClient.Get<CardDto>($"{_config.ApiUrl}/{id}");
        }

        public Task<CardDto> CardByName(string name)
        {
            return _restClient.Get<CardDto>($"{_config.ApiUrl}/api/Cards/{name}");
        }

        public async Task<CardDto> Add(AddCardCommand command)
        {
            var createdUri = await _restClient.Post($"{_config.ApiUrl}/api/Cards", command);

            return await _restClient.Get<CardDto>(createdUri.AbsoluteUri);
        }

        public Task<CardDto> Update(UpdateCardCommand command)
        {
            return _restClient.Put<CardDto>($"{_config.ApiUrl}/api/Cards", command);
        }
    }
}