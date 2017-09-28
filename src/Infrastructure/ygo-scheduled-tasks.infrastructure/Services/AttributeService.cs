using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application;
using ygo_scheduled_tasks.application.Client;
using ygo_scheduled_tasks.application.Dto;
using ygo_scheduled_tasks.application.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public AttributeService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Task<ICollection<AttributeDto>> GetAll()
        {
            return _restClient.Get<ICollection<AttributeDto>>($"{_config.ApiUrl}/api/attributes");
        }
    }
}