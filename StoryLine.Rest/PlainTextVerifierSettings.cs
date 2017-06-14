using System;
using StoryLine.Rest.Expectations;

namespace StoryLine.Rest
{
    public class PlainTextVerifierSettings
    {
        private IStringContentComparer _contentComparer = new StringContentComparer();

        public IStringContentComparer ContentComparer
        {
            get => _contentComparer;
            set => _contentComparer = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}