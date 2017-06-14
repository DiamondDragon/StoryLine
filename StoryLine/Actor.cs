using StoryLine.Contracts;
using StoryLine.Services;

namespace StoryLine
{
    public class Actor : IActor
    {
        public IArtifactCollection Artifacts { get; } = new ArtifactCollection();
    }
}