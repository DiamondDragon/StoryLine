using System;

namespace StoryLine.Wiremock.Builders
{
    public class RequestBuilder : StubBuilderBase
    {
        public RequestBuilder(IApiStubState state) 
            : base(state)
        {
        }

        public RequestBuilder Method(string method)
        {
            if (string.IsNullOrWhiteSpace(method))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(method));

            State.RequestState.Method = method.ToUpper();

            return this;
        }

        public UrlBuilder Url()
        {
            return new UrlBuilder(State);
        }

        public RequestBuilder Url(Func<UrlBuilder, RequestBuilder> url)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            return url(new UrlBuilder(State));
        }

        public PathBuilder Path()
        {
            return new PathBuilder(State);
        }

        public RequestBuilder Path(Func<PathBuilder, RequestBuilder> path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            return path(new PathBuilder(State));
        }

        public ResponseBuilder Response()
        {
            return new ResponseBuilder(State);
        }

        public ReceivedCountBuilder Received()
        {
            return new ReceivedCountBuilder(State);
        }

        public HeaderBuilder Header(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));

            return new HeaderBuilder(State, key);
        }

        public RequestBuilder Header(string key, string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));

            var builder = new HeaderBuilder(State, key);
            return builder.EqualTo(value);
        }

        public BodyBuilder Body()
        {
            return new BodyBuilder(State);
        }

        public RequestBuilder Body(string body)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            var builder = new BodyBuilder(State);
            return builder.EqualTo(body);
        }

        public QueryParamBuilder QueryParam(string key)
        {
            return new QueryParamBuilder(State, key);
        }

        public RequestBuilder QueryParam(string key, string value)
        {
            var builder = new QueryParamBuilder(State, key);
            return builder.EqualTo(value);
        }
    }
}