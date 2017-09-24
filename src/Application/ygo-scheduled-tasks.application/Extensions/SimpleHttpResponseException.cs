using System;
using System.Net;

namespace ygo_scheduled_tasks.application.Extensions
{
    public class SimpleHttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public SimpleHttpResponseException(HttpStatusCode statusCode, string content) : base(content)
        {
            StatusCode = statusCode;
        }
    }
}