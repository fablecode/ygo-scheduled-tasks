using System;
using System.Net;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Dto;

namespace ygo_scheduled_tasks.infrastructure.Client
{
    public class CardRestClient : RestClient<CardDto>
    {
        public Task<CardDto> Get(string apiUrl)
        {
            throw new NotImplementedException();
        }

        public Task<Uri> Post(string apiUrl, object data)
        {
            throw new NotImplementedException();
        }

        public Task<CardDto> Put(string apiUrl, object data)
        {
            throw new NotImplementedException();
        }

        public Task<HttpStatusCode> Delete(string apiUrl)
        {
            throw new NotImplementedException();
        }
    }
}