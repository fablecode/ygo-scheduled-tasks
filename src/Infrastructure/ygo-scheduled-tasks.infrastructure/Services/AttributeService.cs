using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Services;

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

        public Task<ICollection<Attribute>> GetAll()
        {
            return _restClient.Get<ICollection<Attribute>>($"{_config.ApiUrl}/api/attributes");
        }
    }
}