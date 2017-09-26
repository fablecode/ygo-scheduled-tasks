using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using StructureMap;
using ygo_scheduled_tasks.application;
using ygo_scheduled_tasks.application.Dto;
using ygo_scheduled_tasks.application.Extensions;
using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.infrastructure.HttpHandlers
{
    public class OAuthBearerTokenHandler : DelegatingHandler
    {
        private IConfig _config;
        private string _authToken;

        public OAuthBearerTokenHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put || request.Method == HttpMethod.Delete)
            {
                _config = new Container().GetInstance<IConfig>();

                if (string.IsNullOrWhiteSpace(_authToken))
                {
                    using (HttpClient tokenHttpClient = new HttpClient())
                    {
                        var credentials = new Login {Email = _config.OAuthEmail, Password = _config.OAuthPassword};
                        tokenHttpClient
                            .PostAsJsonAsync(_config.ApiUrl + "/api/accounts/token", credentials, cancellationToken)
                            .ContinueWith(async (requestTask) =>
                            {
                                var response = await requestTask;

                                await response.EnsureSuccessStatusCodeAsync();

                                var responseTokenObject = await response.Content.ReadAsAsync<OAuthBearerTokenDto>(cancellationToken);

                                _authToken = responseTokenObject.Token;

                            }, cancellationToken);
                    }
                }

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}