using System;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StoryLine.Rest.Expectations;
using StoryLine.Rest.Services;

namespace StoryLine.Rest
{
    public static class Config
    {
        private static Encoding _defaultEncoding = Encoding.UTF8;
        private static IResponseToTextConverter _responseToTextConverter = new ResponseToTextConverter(
            new ContentTypeProvider());
        private static readonly ServiceRegistry RerviceRegistry = new ServiceRegistry();
        private static readonly ResourceAccessmblyProvider ResourceAccessmblyProvider = new ResourceAccessmblyProvider();
        private static IResourceContentProvider _resourceContentProvider = new ResourceContentProvider(
            ResourceAccessmblyProvider);
 private static IRestClient _restClient = new RestClient(
            new HttpClientFactory(
                RerviceRegistry),
            new RequestMessageFactory(),
            new ResponseFactory()
        );

        private static Func<JsonVerifierSettings, ITextVerifier> _jsonVerifierFactory = x => new JsonVerifier(x);
        private static Func<PlainTextVerifierSettings, ITextVerifier> _plainTextVerifierFactory = x => new PlainTextVerifier(x);

        private static JsonSerializerSettings _defaultJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public static Func<JsonVerifierSettings, ITextVerifier> JsonVerifierFactory
        {
            get => _jsonVerifierFactory;
            set => _jsonVerifierFactory = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static Func<PlainTextVerifierSettings, ITextVerifier> PlainTextVerifierFactory
        {
            get => _plainTextVerifierFactory;
            set => _plainTextVerifierFactory = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static IResourceContentProvider ResourceContentProvider
        {
            get => _resourceContentProvider;
            set => _resourceContentProvider = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static IResponseToTextConverter ResponseToTextConverter
        {
            get => _responseToTextConverter;
            set => _responseToTextConverter = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static JsonSerializerSettings DefaultJsonSerializerSettings
        {
            get => _defaultJsonSerializerSettings;
            set => _defaultJsonSerializerSettings = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static Encoding DefaultEncoding
        {
            get => _defaultEncoding;
            set => _defaultEncoding = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static IRestClient RestClient
        {
            get => _restClient;
            set => _restClient = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static void SetResourceAssemblies(params Assembly[] assemblies)
        {
            ResourceAccessmblyProvider.Assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
        }

        public static void AddServiceEndpont(string service, string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(service))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(service));
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(baseUrl));

            RerviceRegistry.Add(new ServiceConfig(service, baseUrl));
        }
    }
}
