using StoryLine.Wiremock.Contracts;
using StoryLine.Wiremock.Services;

namespace StoryLine.Wiremock.Builders
{
    public interface IApiStubState
    {
        Request RequestState { get; }
        Response ResponseState { get; }
        ITimes RequestCount { get; set; }
    }
}