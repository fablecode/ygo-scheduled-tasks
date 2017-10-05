using System;
using System.Net;
using System.Threading.Tasks;

namespace ygo_scheduled_tasks.domain.Client
{
    public interface IRestClient
    {
        Task<T> Get<T>(string apiUrl);
        Task<Uri> Post<T>(string apiUrl, T data);
        Task<TResponse> Put<TRequest, TResponse>(string apiUrl, TRequest data);
        Task<HttpStatusCode> Delete(string apiUrl);
    }
}