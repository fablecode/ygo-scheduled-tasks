using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application;
using ygo_scheduled_tasks.application.Client;
using ygo_scheduled_tasks.application.Dto;
using ygo_scheduled_tasks.application.Services;

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

        public Task<ICollection<TypeDto>> GetAll()
        {
            return _restClient.Get<ICollection<TypeDto>>($"{_config.ApiUrl}/api/types");
        }
    }
}