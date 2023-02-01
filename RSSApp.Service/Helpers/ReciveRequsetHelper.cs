using System.Net;

namespace RSSApp.Service.Helpers
{
    internal class ReciveRequsetHelper
    {
        private static string _URLString;

        public static string ReciveToRequest(string urlString)
        {
            _URLString = urlString;

            return ReadStream();
        }

        private static string ReadStream()
        {
            var stream = CreateStream();

            if (stream != null)
                return new StreamReader(stream).ReadToEnd();
            else
                throw new InvalidDataException($"Nothing was found on this link: {nameof(_URLString)}");
        }

        private static Stream CreateStream()
        {
            return CreateWebResponse().GetResponseStream();
        }

        private static HttpWebResponse CreateWebResponse()
        {
            return CreateRequest().GetResponse() as HttpWebResponse;
        }

        private static HttpWebRequest CreateRequest()
        {
            var _request = WebRequest.Create(_URLString) as HttpWebRequest;
            _request.Method = "GET";
            return _request;
        }
    }
}
