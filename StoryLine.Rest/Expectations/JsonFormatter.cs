using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StoryLine.Rest.Expectations
{
    public class JsonFormatter : IJsonFormatter
    {
        public string Format(string content, params string[] propertiesToIgnore)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            var jsonObject = JToken.Parse(content);

            if (propertiesToIgnore == null || !propertiesToIgnore.Any())
                return jsonObject.ToString(Formatting.Indented);

            var jTokensToRemove = new List<JToken>();

            foreach (var propSelector in propertiesToIgnore)
            {
                jTokensToRemove.AddRange(jsonObject.SelectTokens(propSelector));
            }

            foreach (var token in jTokensToRemove)
            {
                token.Parent.Remove();
            }

            return jsonObject.ToString(Formatting.Indented);
        }
    }
}