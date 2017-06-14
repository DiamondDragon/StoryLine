using System;
using System.Collections.Generic;
using System.Linq;
using StoryLine.Contracts;

namespace StoryLine.Services
{
    public class ArtifactCollection : IArtifactCollection
    {
        private readonly List<object> _artifacts = new List<object>();

        public void Add(object artifact)
        {
            if (artifact == null)
                throw new ArgumentNullException(nameof(artifact));

            lock (_artifacts)
            {
                _artifacts.Insert(0, artifact);
            }
        }

        public T Get<T>(Func<T, bool> predicate = null)
        {
            lock (_artifacts)
            {
                return predicate == null ?
                    _artifacts.OfType<T>().FirstOrDefault() : 
                    _artifacts.OfType<T>().FirstOrDefault(predicate);
            }
        }

        public T[] GetAll<T>(Func<T, bool> predicate = null)
        {
            lock (_artifacts)
            {
                return predicate == null ?
                    _artifacts.OfType<T>().ToArray() :
                    _artifacts.OfType<T>().Where(predicate).ToArray();
            }
        }

        public void Clear()
        {
            lock (_artifacts)
            {
                _artifacts.Clear();
            }
        }
    }
}