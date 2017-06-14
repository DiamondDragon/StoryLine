namespace StoryLine.Wiremock.Builders
{
    public class UrlBuilder : StubBuilderBase
    {
        public UrlBuilder(IApiStubState state) 
            : base(state)
        {
        }

        public RequestBuilder EqualTo(string url)
        {
            State.RequestState.Url = url;
            return new RequestBuilder(State);
        }

        public RequestBuilder Matching(string pattern)
        {
            State.RequestState.UrlPattern = pattern;

            return new RequestBuilder(State);
        }
    }
}