using System;
using System.Collections.Generic;

namespace StoryLine.Wiremock.Services
{
    public class ServiceRegistry : IServiceRegistry
    {
        private static readonly IDictionary<Type, object> Registry = new Dictionary<Type, object>();

        public void Reset()
        {
            Registry.Clear();
        }

        public void Register<T>(T instance)
            where T : class
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            Registry.Add(typeof(T), instance);
        }

        public T Get<T>()
        {
            return (T)Registry[typeof(T)];
        }
    }
}
