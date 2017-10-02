using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application;
using ygo_scheduled_tasks.application.Api;
using ygo_scheduled_tasks.application.Client;
using ygo_scheduled_tasks.application.Dto;

namespace ygo_scheduled_tasks.infrastructure.Api
{
    public class LinkArrowService : ILinkArrowService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public LinkArrowService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Task<ICollection<LinkArrowDto>> GetAll()
        {
            return _restClient.Get<ICollection<LinkArrowDto>>($"{_config.ApiUrl}/api/linkarrows");
        }
    }
}