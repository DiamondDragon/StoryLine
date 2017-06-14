using System;

namespace StoryLine.Rest.Actions
{
    public class TextBodyBuilder
    {
        private readonly HttpRequest _request;

        public TextBodyBuilder(HttpRequest request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public HttpRequest FromResource()
        {
            return _request;
        }
    }
}