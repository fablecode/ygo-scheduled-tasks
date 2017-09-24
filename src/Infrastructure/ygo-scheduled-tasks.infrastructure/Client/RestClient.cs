﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Client;
using ygo_scheduled_tasks.application.Extensions;

namespace ygo_scheduled_tasks.infrastructure.Client
{
    public class RestClient<T> : IRestClient<T>
    {
        private static readonly HttpClient client = new HttpClient();

        static RestClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> Get(string apiUrl)
        {
            var response = await client.GetAsync(apiUrl);
            await response.EnsureSuccessStatusCodeAsync();

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<Uri> Post(string apiUrl, object data)
        {
            var response = await client.PostAsJsonAsync(apiUrl, data);
            await response.EnsureSuccessStatusCodeAsync();

            return response.Headers.Location;
        }

        public async Task<T> Put(string apiUrl, object data)
        {
            var response = await client.PutAsJsonAsync(apiUrl, data);
            await response.EnsureSuccessStatusCodeAsync();

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<HttpStatusCode> Delete(string apiUrl)
        {
            var response = await client.DeleteAsync(apiUrl);
            return response.StatusCode;
        }
    }
}