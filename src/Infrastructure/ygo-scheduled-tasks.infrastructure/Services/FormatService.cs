using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class FormatService : IFormatService
    {
        private readonly IRestClient _restClient;
        private readonly string _apiUrl;

        public FormatService(IConfig config, IRestClient restClient)
        {
            _restClient = restClient;
            _apiUrl = $"{config.ApiUrl}/api/Formats";
        }

        public Task<Format> FormatByAcronym(string acronym)
        {
            return _restClient.Get<Format>($"{_apiUrl}/{acronym}");
        }
    }
}