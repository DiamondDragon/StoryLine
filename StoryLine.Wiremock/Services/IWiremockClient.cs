using StoryLine.Wiremock.Contracts;

namespace StoryLine.Wiremock.Services
{
    public interface IWiremockClient
    {
        MappingResult Create(Mapping mapping);
        int Count(Request request);
        void Reset(string id);
        void ResetAll();
    }
}