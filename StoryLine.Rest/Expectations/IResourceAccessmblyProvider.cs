using System.Collections.Generic;
using System.Reflection;

namespace StoryLine.Rest.Expectations
{
    public interface IResourceAccessmblyProvider
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}