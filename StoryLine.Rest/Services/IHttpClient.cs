using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoryLine.Rest.Services
{
    public interface IHttpClient : IDisposable
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}