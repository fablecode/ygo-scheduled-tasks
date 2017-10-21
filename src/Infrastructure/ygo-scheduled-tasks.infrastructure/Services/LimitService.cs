using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class LimitService : ILimitService
    {
        private readonly IRestClient _restClient;
        private readonly string _apiUrl;

        public LimitService(IConfig config, IRestClient restClient)
        {
            _restClient = restClient;
            _apiUrl = $"{config.ApiUrl}/api/limits";
        }

        public Task<ICollection<Limit>> GetAll()
        {
            return _restClient.Get<ICollection<Limit>>(_apiUrl);
        }
    }
}