using System.Net;
using System.Net.Http.Formatting;

namespace RSSApp.Service.Extentions
{
    public static class CreateHttpResponseMessageExtension
    {
        public static HttpResponseMessage CreateResponseMessage(this HttpResponseMessage _httpResponseMessage, HttpStatusCode statusCode, string msg)
        {
            _httpResponseMessage.StatusCode = statusCode;
            _httpResponseMessage.ReasonPhrase = msg;
            return _httpResponseMessage;
        }
    }
}
