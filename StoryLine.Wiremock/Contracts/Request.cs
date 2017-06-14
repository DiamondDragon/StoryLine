using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using StoryLine.Wiremock.Helpers;

namespace StoryLine.Wiremock.Contracts
{
    public class Request
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public string UrlPath { get; set; }
        public string UrlPattern { get; set; }
        public string UrlPathPattern { get; set; }

        [JsonConverter(typeof(BodyPatternsConverter))]
        public List<KeyValuePair<string, object>> BodyPatterns { get; } = new List<KeyValuePair<string, object>>();
        public Dictionary<string, Dictionary<string, string>> QueryParameters { get; } = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, Dictionary<string, string>> Headers { get; } = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
    };
}