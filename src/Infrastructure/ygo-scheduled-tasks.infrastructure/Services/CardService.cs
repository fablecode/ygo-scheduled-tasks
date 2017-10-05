using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Client;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class CardService : ICardService
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public CardService(IConfig config, IRestClient restClient)
        {
            _config = config;
            _restClient = restClient;
        }

        public Task<Card> CardById(long id)
        {
            return _restClient.Get<Card>($"{_config.ApiUrl}/{id}");
        }

        public Task<Card> CardByName(string name)
        {
            return _restClient.Get<Card>($"{_config.ApiUrl}/api/Cards/{name}");
        }

        public async Task<Card> Add(AddCardCommand command)
        {
            var createdUri = await _restClient.Post($"{_config.ApiUrl}/api/Cards", command);

            return await _restClient.Get<Card>(createdUri.AbsoluteUri);
        }

        public Task<Card> Update(UpdateCardCommand command)
        {
            return _restClient.Put<UpdateCardCommand, Card>($"{_config.ApiUrl}/api/Cards", command);
        }
    }
}