using StoryLine.Contracts;
using StoryLine.Wiremock.Builders;

namespace StoryLine.Wiremock.Actions
{
    public class StubResponse : IActionBuilder
    {
        private readonly IApiStubState _apiStubState = new ApiStubState();

        IAction IActionBuilder.Build()
        {
            return new StubResponseAction(_apiStubState);
        }

        public RequestBuilder Request()
        {
            return new RequestBuilder(_apiStubState);
        }
    }
}
