using System;
using System.Net.Http;

namespace StoryLine.Rest.Services
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly IServiceRegistry _serviceRegistry;

        public HttpClientFactory(IServiceRegistry serviceRegistry)
        {
            _serviceRegistry = serviceRegistry ?? throw new ArgumentNullException(nameof(serviceRegistry));
        }

        public IHttpClient Create(string service)
        {
            var httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
            };

            var config = service == null ? null : _serviceRegistry.GetConfig(service);

            var client = new HttpClientWrapper(httpClientHandler);

            if (config == null)
                return client;

            client.BaseAddress = new Uri(config.BaseAddress.TrimEnd('/'), UriKind.Absolute);

            return client;
        }
    }
}