using System.Net.Http;
using System.Threading.Tasks;

namespace ygo_scheduled_tasks.application.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task EnsureSuccessStatusCodeAsync(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var content = await response.Content.ReadAsStringAsync();

            response.Content?.Dispose();

            throw new SimpleHttpResponseException(response.StatusCode, content);
        }
    }
}