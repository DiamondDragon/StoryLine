using System;

namespace StoryLine.Contracts
{
    public interface IArtifactCollection
    {
        void Add(object artifact);
        T Get<T>(Func<T, bool> predicate = null);
        T[] GetAll<T>(Func<T, bool> predicate = null);
        void Clear();
    }
}