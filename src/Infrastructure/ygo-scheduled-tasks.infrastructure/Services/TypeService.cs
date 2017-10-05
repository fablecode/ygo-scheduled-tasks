using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Client;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class TypeService : ITypeService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public TypeService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Task<ICollection<Type>> GetAll()
        {
            return _restClient.Get<ICollection<Type>>($"{_config.ApiUrl}/api/types");
        }
    }
}