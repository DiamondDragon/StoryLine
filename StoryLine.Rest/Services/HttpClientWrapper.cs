using System.Net.Http;

namespace StoryLine.Rest.Services
{
    public class HttpClientWrapper : HttpClient, IHttpClient
    {
        public HttpClientWrapper()
        {
        }

        public HttpClientWrapper(HttpMessageHandler handler)
            : base(handler)
        {
            
        }
    }
}