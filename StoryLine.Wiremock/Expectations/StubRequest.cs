using StoryLine.Contracts;
using StoryLine.Wiremock.Builders;

namespace StoryLine.Wiremock.Expectations
{
    public class StubRequest : IExpectationBuilder
    {
        private readonly IApiStubState _state = new ApiStubState();

        public RequestBuilder Request()
        {
            return new RequestBuilder(_state);
        }

        IExpectation IExpectationBuilder.Build()
        {
            return new StubRequestExpectation(_state);
        }
    }
}
