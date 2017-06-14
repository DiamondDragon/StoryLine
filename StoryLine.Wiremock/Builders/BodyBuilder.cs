using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StoryLine.Wiremock.Builders
{
    public class BodyBuilder : StubBuilderBase
    {
        public BodyBuilder(IApiStubState state)
            : base(state)
        {
        }

        public RequestBuilder Containing(string pattern)
        {
            return AddBody("contains", pattern);
        }

        public RequestBuilder EqualTo(string value)
        {
            return AddBody("equalTo", value);
        }

        public RequestBuilder Matching(string pattern)
        {
            return AddBody("matches", pattern);
        }

        public RequestBuilder NotMatching(string pattern)
        {
            return AddBody("doesNotMatch", pattern);
        }

        public RequestBuilder EqualToJson(string json, bool ignoreArrayOrder = true, bool ignoreExtraElements = true)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException(nameof(json));

            return AddBody("equalToJson", new
            {
                equalToJson = json,
                ignoreArrayOrder = true,
                ignoreExtraElements = true,
            });
        }

        public RequestBuilder EqualToJsonBody(object body, bool ignoreArrayOrder = true, bool ignoreExtraElements = true)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            return EqualToJsonBody(body, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            },
            ignoreArrayOrder, 
            ignoreExtraElements);
        }

        public RequestBuilder EqualToJsonBody(object body, JsonSerializerSettings settings, bool ignoreArrayOrder = true, bool ignoreExtraElements = true)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            return EqualToJson(JsonConvert.SerializeObject(body, settings), ignoreArrayOrder, ignoreExtraElements);
        }

        public RequestBuilder MatchingJsonPath(string jsonPath)
        {
            if (string.IsNullOrEmpty(jsonPath))
                throw new ArgumentNullException(nameof(jsonPath));

            return AddBody("matchesJsonPath", jsonPath);
        }

        public RequestBuilder EqualToXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException(nameof(xml));

            return AddBody("equalToXml", xml);
        }

        public RequestBuilder MatchingXPath(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            return AddBody("matchesXPath", xPath);
        }

        public RequestBuilder MatchingXPath(string xPath, IDictionary<string, string> xPathNamespaces)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));
            if (xPathNamespaces == null)
                throw new ArgumentNullException(nameof(xPathNamespaces));

            return AddBody("matchesXPath", new
            {
                matchesXPath = xPath,
                withXPathNamespaces = xPathNamespaces
            });
        }

        internal RequestBuilder AddBody(string comparer, object value)
        {
            State.RequestState.BodyPatterns.Add(new KeyValuePair<string, object>(comparer, value));
            return new RequestBuilder(State);
        }
    }
}