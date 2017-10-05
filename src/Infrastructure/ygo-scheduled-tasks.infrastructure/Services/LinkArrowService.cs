using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
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

        public Task<ICollection<LinkArrow>> GetAll()
        {
            return _restClient.Get<ICollection<LinkArrow>>($"{_config.ApiUrl}/api/linkarrows");
        }
    }
}