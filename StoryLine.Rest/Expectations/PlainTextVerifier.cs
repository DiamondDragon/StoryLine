using System;

namespace StoryLine.Rest.Expectations
{
    public class PlainTextVerifier : ITextVerifier
    {
        private readonly PlainTextVerifierSettings _settings;

        public PlainTextVerifier(PlainTextVerifierSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }


        public void Verify(string expectedValue, string actualValue)
        {
            if (actualValue == expectedValue)
                return;

            _settings.ContentComparer.Verify(expectedValue ?? string.Empty, actualValue ?? string.Empty);
           
        }
    }
}