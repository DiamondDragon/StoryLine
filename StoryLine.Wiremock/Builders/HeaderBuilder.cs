using System;
using System.Collections.Generic;

namespace StoryLine.Wiremock.Builders
{
    public class HeaderBuilder : StubBuilderBase
    {
        private readonly string _key;

        public HeaderBuilder(IApiStubState state, string key)
            : base(state)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            _key = key;
        }

        public RequestBuilder Containing(string value)
        {
            return AddToHeader(_key, "contains", value);
        }

        public RequestBuilder EqualTo(string value)
        {
            return AddToHeader(_key, "equalTo", value);
        }

        public RequestBuilder Matching(string pattern)
        {
            return AddToHeader(_key, "matches", pattern);
        }

        public RequestBuilder NotMatching(string pattern)
        {
            return AddToHeader(_key, "doesNotMatch", pattern);
        }

        internal RequestBuilder AddToHeader(string key, string comparer, string value)
        {
            if (!State.RequestState.Headers.ContainsKey(key))
                State.RequestState.Headers.Add(key, new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));

            State.RequestState.Headers[key].Add(comparer, value);

            return new RequestBuilder(State);
        }
    }
}