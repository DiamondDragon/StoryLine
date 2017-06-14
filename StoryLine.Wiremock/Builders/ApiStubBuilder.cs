namespace StoryLine.Wiremock.Builders
{
    public class ApiStubBuilder : StubBuilderBase
    {
        public ApiStubBuilder(IApiStubState state)
            : base(state)
        {
        }

        public RequestBuilder Request()
        {
            return new RequestBuilder(State);
        }
    }
}
