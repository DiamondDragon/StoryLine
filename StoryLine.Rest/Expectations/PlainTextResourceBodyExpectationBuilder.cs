using System;

namespace StoryLine.Rest.Expectations
{
    public class PlainTextResourceBodyExpectationBuilder
    {
        private readonly HttpResponse _buider;
        private static IResourceContentProvider ContentProvider => Config.ResourceContentProvider;

        public PlainTextResourceBodyExpectationBuilder(HttpResponse buider)
        {
            _buider = buider ?? throw new ArgumentNullException(nameof(buider));
        }

        public HttpResponse ResourceFile()
        {
            var content = ContentProvider.GetContent(null);
            if (string.IsNullOrEmpty(content))
                throw new InvalidOperationException("Default resource was not found");

            _buider.RequestExpectation(
                new BodyContentExpectation(
                    content,
                    Config.PlainTextVerifierFactory(new PlainTextVerifierSettings())));

            return _buider;
        }

        public HttpResponse ResourceFile(string resourceName)
        {
            if (string.IsNullOrEmpty(resourceName))
                throw new ArgumentNullException(nameof(resourceName));

            var content = ContentProvider.GetContent(resourceName);
            if (string.IsNullOrEmpty(content))
                throw new InvalidOperationException($"Resource \"{resourceName}\" was not found");

            _buider.RequestExpectation(
                new BodyContentExpectation(
                    content,
                    Config.PlainTextVerifierFactory(new PlainTextVerifierSettings())));

            return _buider;
        }
    }
}