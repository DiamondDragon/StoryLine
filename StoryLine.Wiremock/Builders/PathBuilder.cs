namespace StoryLine.Wiremock.Builders
{
    public class PathBuilder : StubBuilderBase
    {
        public PathBuilder(IApiStubState state) 
            : base(state)
        {
        }

        public RequestBuilder EqualTo(string path)
        {
            State.RequestState.UrlPath = path;

            return new RequestBuilder(State);
        }
        public RequestBuilder Matching(string pattern)
        {
            State.RequestState.UrlPathPattern = pattern;

            return new RequestBuilder(State);
        }
    }
}