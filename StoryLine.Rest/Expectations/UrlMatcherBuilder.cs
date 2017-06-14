using System;

namespace StoryLine.Rest.Expectations
{
    public class UrlMatcherBuilder
    {
        private readonly HttpResponse _httpResponse;

        public UrlMatcherBuilder(HttpResponse httpResponse)
        {
            _httpResponse = httpResponse ?? throw new ArgumentNullException(nameof(httpResponse));
        }

        public HttpResponse Containing(string url)
        {
            return _httpResponse;
        }

        public HttpResponse Matching(string url)
        {
            return _httpResponse;
        }

        public HttpResponse EqualTo(string url)
        {
            return _httpResponse;
        }
    }
}