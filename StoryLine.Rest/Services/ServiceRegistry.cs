using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryLine.Rest.Services
{
    public class ServiceRegistry : IServiceRegistry
    {
        private readonly List<IServiceConfig> _configs = new List<IServiceConfig>();

        public void Add(IServiceConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _configs.Add(config);
        }

        public IServiceConfig GetConfig(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                throw new ArgumentException("Value cannot be null or empty.", nameof(serviceName));

            return _configs.FirstOrDefault(x => x.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
        }
    }
}