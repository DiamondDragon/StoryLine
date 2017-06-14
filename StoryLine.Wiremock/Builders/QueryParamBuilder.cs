using System;
using System.Collections.Generic;

namespace StoryLine.Wiremock.Builders
{
    public class QueryParamBuilder : StubBuilderBase
    {
        private readonly string _key;

        public QueryParamBuilder(IApiStubState state, string key)
            : base(state)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            _key = key;
        }

        public RequestBuilder Containing(string pattern)
        {
            return AddToQueryParameters(_key, "contains", pattern);
        }

        public RequestBuilder EqualTo(string pattern)
        {
            return AddToQueryParameters(_key, "equalTo", pattern);
        }

        public RequestBuilder Matching(string pattern)
        {
            return AddToQueryParameters(_key, "matches", pattern);
        }

        public RequestBuilder NotMatching(string pattern)
        {
            return AddToQueryParameters(_key, "doesNotMatch", pattern);
        }

        internal RequestBuilder AddToQueryParameters(string key, string comparer, string value)
        {
            if (!State.RequestState.QueryParameters.ContainsKey(key))
                State.RequestState.QueryParameters.Add(key, new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));

            State.RequestState.QueryParameters[key].Add(comparer, value);

            return new RequestBuilder(State);
        }
    }
}