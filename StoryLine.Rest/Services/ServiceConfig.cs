using System;

namespace StoryLine.Rest.Services
{
    public class ServiceConfig : IServiceConfig
    {
        public string Service { get; }
        public string BaseAddress { get; }

        public ServiceConfig(string service, string baseAddress)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(baseAddress));

            Service = service;
            BaseAddress = baseAddress;
        }
    }
}