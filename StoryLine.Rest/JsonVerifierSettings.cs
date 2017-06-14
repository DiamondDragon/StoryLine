using System;
using StoryLine.Rest.Expectations;

namespace StoryLine.Rest
{
    public class JsonVerifierSettings
    {
        private string[] _ignoredProperties = new string[0];
        private IJsonFormatter _jsonFormatter = new JsonFormatter();
        private IStringContentComparer _contentComparer = new StringContentComparer();

        public string[] IgnoredProperties
        {
            get => _ignoredProperties;
            set => _ignoredProperties = value ?? throw new ArgumentNullException(nameof(value));
        }

        public IStringContentComparer ContentComparer
        {
            get => _contentComparer;
            set => _contentComparer = value ?? throw new ArgumentNullException(nameof(value));
        }

        public IJsonFormatter JsonFormatter
        {
            get => _jsonFormatter;
            set => _jsonFormatter = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}