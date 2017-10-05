using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.Extensions;

namespace ygo_scheduled_tasks.infrastructure.HttpHandlers
{
    public class OAuthBearerTokenHandler : DelegatingHandler
    {
        private readonly IConfig _config;
        private string _authToken;

        public OAuthBearerTokenHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            _config = (Config) ConfigurationManager.GetSection("ygo-settings");
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put || request.Method == HttpMethod.Delete)
            {
                if (string.IsNullOrWhiteSpace(_authToken))
                {
                    using (var tokenHttpClient = new HttpClient())
                    {
                        var credentials = new Login { Email = _config.OAuthEmail, Password = _config.OAuthPassword };
                        var response = await tokenHttpClient.PostAsJsonAsync(_config.ApiUrl + "/api/accounts/token", credentials);

                        await response.EnsureSuccessAsync();

                        var responseTokenObject = await response.Content.ReadAsAsync<OAuthBearerToken>();

                        _authToken = responseTokenObject.Token;
                    }
                }

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}