using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Client;
using ygo_scheduled_tasks.application.Extensions;
using ygo_scheduled_tasks.infrastructure.HttpHandlers;

namespace ygo_scheduled_tasks.infrastructure.Client
{
    public class RestClient : IRestClient
    {
        private static readonly HttpClient client = new HttpClient(new OAuthBearerTokenHandler(new HttpClientHandler()));

        static RestClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> Get<T>(string apiUrl)
        {
            var response = await client.GetAsync(apiUrl);
            await response.EnsureSuccessAsync();

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<Uri> Post<T>(string apiUrl, T data)
        {
            var response = await client.PostAsJsonAsync(apiUrl, data);
            await response.EnsureSuccessAsync();

            return response.Headers.Location;
        }

        public async Task<TResponse> Put<TRequest, TResponse>(string apiUrl, TRequest data)
        {
            var response = await client.PutAsJsonAsync(apiUrl, data);
            await response.EnsureSuccessAsync();

            return await response.Content.ReadAsAsync<TResponse>();
        }

        public async Task<HttpStatusCode> Delete(string apiUrl)
        {
            var response = await client.DeleteAsync(apiUrl);
            return response.StatusCode;
        }
    }
}