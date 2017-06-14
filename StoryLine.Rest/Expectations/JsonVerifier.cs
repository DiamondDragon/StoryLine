using System;

namespace StoryLine.Rest.Expectations
{
    public class JsonVerifier : ITextVerifier
    {
        private readonly JsonVerifierSettings _jsonVerifierSettings;

        public JsonVerifier(JsonVerifierSettings jsonVerifierSettings)
        {
            _jsonVerifierSettings = jsonVerifierSettings ?? throw new ArgumentNullException(nameof(jsonVerifierSettings));
        }

        public void Verify(string expectedValue, string actualValue)
        {
            if (expectedValue == actualValue)
                return;

            _jsonVerifierSettings.ContentComparer.Verify(
                _jsonVerifierSettings.JsonFormatter.Format(expectedValue ?? string.Empty, _jsonVerifierSettings.IgnoredProperties), 
                _jsonVerifierSettings.JsonFormatter.Format(actualValue ?? string.Empty, _jsonVerifierSettings.IgnoredProperties));
        }
    }
}