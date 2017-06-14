using System;

namespace StoryLine.Rest.Actions
{
    public class BinaryBodyBuilder
    {
        private readonly HttpRequest _request;

        public BinaryBodyBuilder(HttpRequest request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public HttpRequest FromResource()
        {
            return _request;
        }
    }
}