using System;
using Newtonsoft.Json;
using StoryLine.Wiremock.Helpers;
using StoryLine.Wiremock.Services;

namespace StoryLine.Wiremock
{
    public static class Config
    {
        private static readonly WiremockConfig WiremockConfig = new WiremockConfig();
        private static readonly RestClient RestClient = new RestClient();
        private static IWiremockClient _wiremockClient = new WiremockClient(WiremockConfig, RestClient);
        private static JsonSerializerSettings _defaultJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCaseExceptDictionaryKeysResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public static IWiremockClient Client
        {
            get => _wiremockClient;
            set => _wiremockClient = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static JsonSerializerSettings DefaultJsonSerializerSettings
        {
            get => _defaultJsonSerializerSettings;
            set => _defaultJsonSerializerSettings = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static void SetBaseAddress(string baseAddress)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(baseAddress));

            WiremockConfig.ServerAddress = baseAddress;
        }

        public static void ResetAll()
        {
            Client.ResetAll();
        }
    }
}
