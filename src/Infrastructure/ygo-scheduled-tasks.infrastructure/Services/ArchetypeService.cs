using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class ArchetypeService : IArchetypeService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public ArchetypeService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Task<Archetype> ArchetypeById(long id)
        {
            return _restClient.Get<Archetype>($"{_config.ApiUrl}/api/Archetypes/{id}");
        }

        public Task<Archetype> ArchetypeByName(string name)
        {
            return _restClient.Get<Archetype>($"{_config.ApiUrl}/api/Archetypes/named",new Dictionary<string, string>{ {"name", name}});
        }

        public async Task<Archetype> Add(AddArchetypeCommand command)
        {
            var createdUri = await _restClient.Post($"{_config.ApiUrl}/api/Archetypes", command);

            return await _restClient.Get<Archetype>(createdUri.AbsoluteUri);
        }

        public Task<Archetype> Update(UpdateArchetypeCommand command)
        {
            return _restClient.Put<UpdateArchetypeCommand, Archetype>($"{_config.ApiUrl}/api/Archetypes", command);
        }
    }
}