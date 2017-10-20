﻿using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class YugiohBanlistService : IYugiohBanlistService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public YugiohBanlistService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Banlist AddOrUpdate(YugiohBanlist banlist)
        {
            throw new System.NotImplementedException();
        }
    }
}