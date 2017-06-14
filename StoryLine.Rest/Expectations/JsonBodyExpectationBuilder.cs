using System;

namespace StoryLine.Rest.Expectations
{
    public class JsonBodyExpectationBuilder
    {
        private readonly HttpResponse _builder;

        public JsonBodyExpectationBuilder(HttpResponse builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public HttpResponse Matching(string expectedContent, params string[] propertiesToIgnore)
        {
            if (propertiesToIgnore == null)
                throw new ArgumentNullException(nameof(propertiesToIgnore));
            if (string.IsNullOrEmpty(expectedContent))
                throw new ArgumentException("Value cannot be null or empty.", nameof(expectedContent));

            var config = new JsonVerifierSettings
            {
                IgnoredProperties = propertiesToIgnore
            };

            _builder.RequestExpectation(
                new BodyContentExpectation(
                    expectedContent,
                    Config.JsonVerifierFactory(config)));

            return _builder;
        }

        public JsonResourceBodyExpectationBuilder Matching()
        {
            return new JsonResourceBodyExpectationBuilder(_builder);
        }
    }
}