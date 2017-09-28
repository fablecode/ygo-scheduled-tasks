using System;
using System.Net;
using System.Threading.Tasks;

namespace ygo_scheduled_tasks.application.Client
{
    public interface IRestClient
    {
        Task<T> Get<T>(string apiUrl);
        Task<Uri> Post<T>(string apiUrl, T data);
        Task<T> Put<T>(string apiUrl, object data);
        Task<HttpStatusCode> Delete(string apiUrl);
    }
}