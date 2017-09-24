using System;
using System.Net;
using System.Threading.Tasks;

namespace ygo_scheduled_tasks.application.Client
{
    public interface IRestClient<T>
    {
        Task<T> Get(string apiUrl);
        Task<Uri> Post(string apiUrl, object data);
        Task<T> Put(string apiUrl, object data);
        Task<HttpStatusCode> Delete(string apiUrl);
    }
}