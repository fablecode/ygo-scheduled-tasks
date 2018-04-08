using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class ArchetypeCardsService : IArchetypeCardsService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public ArchetypeCardsService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Task<IEnumerable<ArchetypeCard>> Update(long archetypeId, IEnumerable<string> cards)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ArchetypeCard>> Update(UpdateArchetypeCardsCommand updateCommand)
        {
            return _restClient.Put<UpdateArchetypeCardsCommand, IEnumerable<ArchetypeCard>>($"{_config.ApiUrl}/api/ArchetypeCards", updateCommand);
        }
    }


}