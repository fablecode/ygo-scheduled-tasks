using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class ArchetypeSupportCardsService : IArchetypeSupportCardsService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public ArchetypeSupportCardsService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Task<IEnumerable<ArchetypeCard>> Update(UpdateArchetypeSupportCardsCommand updateCommand)
        {
            return _restClient.Put<UpdateArchetypeSupportCardsCommand, IEnumerable<ArchetypeCard>>($"{_config.ApiUrl}/api/ArchetypeSupportCards", updateCommand);
        }
    }
}