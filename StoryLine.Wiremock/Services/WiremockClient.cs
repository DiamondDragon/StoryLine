using System;
using StoryLine.Wiremock.Contracts;

namespace StoryLine.Wiremock.Services
{
    public class WiremockClient : IWiremockClient
    {
        private readonly IWiremockConfig _config;
        private readonly IRestClient _restClient;

        public WiremockClient(IWiremockConfig config, IRestClient restClient)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            if (restClient == null)
                throw new ArgumentNullException(nameof(restClient));

            _config = config;
            _restClient = restClient;
        }

        public MappingResult Create(Mapping mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException(nameof(mapping));

            return Post<MappingResult>("/__admin/mappings", mapping);
        }

        public int Count(Request request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return Post<RequestCount>("/__admin/requests/count", request).Count;
        }

        public void ResetAll()
        {
            _restClient.PostJson(ToAbsoluteUrl("/__admin/mappings/reset"));
        }

        public void Reset(string id)
        {
            _restClient.Delete(ToAbsoluteUrl("/__admin/mappings/" + id));
        }

        private TResponse Post<TResponse>(string relativeUrl, object body)
        {
            var absoluteUrl = ToAbsoluteUrl(relativeUrl);

            return _restClient.PostJson<TResponse>(absoluteUrl, body);
        }

        private string ToAbsoluteUrl(string relativeUrl)
        {
            var absoluteUrl = _config.ServerAddress;

            if (absoluteUrl.EndsWith("/"))
                absoluteUrl = absoluteUrl.Substring(0, absoluteUrl.Length - 1);

            absoluteUrl += relativeUrl;

            return absoluteUrl;
        }
    }
}
