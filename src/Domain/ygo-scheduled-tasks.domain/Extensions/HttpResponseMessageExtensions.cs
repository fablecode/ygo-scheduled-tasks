using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ygo_scheduled_tasks.domain.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task EnsureSuccessAsync(this HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.InternalServerError)
            {
                return;
            }

            var content = await response.Content.ReadAsStringAsync();

            response.Content?.Dispose();

            throw new SimpleHttpResponseException(response.StatusCode, content);
        }
    }
}