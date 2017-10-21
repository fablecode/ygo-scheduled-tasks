using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class BanlistService : IBanlistService
    {
        private readonly IRestClient _restClient;
        private readonly string _apiUrl;

        public BanlistService(IConfig config, IRestClient restClient)
        {
            _restClient = restClient;
            _apiUrl = $"{config.ApiUrl}/api/Banlists";
        }

        public Task<Banlist> BanlistById(long id)
        {
            return _restClient.Get<Banlist>($"{_apiUrl}/{id}");
        }

        public async Task<Banlist> Add(AddBanlistCommand command)
        {
            var createdUri = await _restClient.Post(_apiUrl, command);

            return await _restClient.Get<Banlist>(createdUri.AbsoluteUri);
        }

        public Task<Banlist> Update(UpdateBanlistCommand command)
        {
            return _restClient.Put<UpdateBanlistCommand, Banlist>(_apiUrl, command);
        }

        public Task<IList<BanlistCard>> Update(long id, UpdateBanlistCardsCommand command)
        {
            return _restClient.Put<UpdateBanlistCardsCommand, IList<BanlistCard>>($"{_apiUrl}/{id}/cards", command);
        }
    }
}