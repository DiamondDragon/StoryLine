using System;
using System.Text.RegularExpressions;

namespace StoryLine.Rest.Expectations
{
    public class UrlPredicateBuilder
    {
        private readonly HttpResponse _response;

        public UrlPredicateBuilder(HttpResponse response)
        {
            _response = response ?? throw new ArgumentNullException(nameof(response));
        }

        public HttpResponse Matching(string pattern, RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(pattern));

            _response.Url(x => Regex.IsMatch(x, pattern, options));

            return _response;
        }

        public HttpResponse EqualTo(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(pattern));

            pattern = pattern.ToLower();

            _response.Url(x => x.Equals(pattern, StringComparison.OrdinalIgnoreCase));

            return _response;
        }

        public HttpResponse StartingWith(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(pattern));

            _response.Url(x => x.StartsWith(pattern, StringComparison.OrdinalIgnoreCase));

            return _response;
        }

        public HttpResponse Containing(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(pattern));

            pattern = pattern.ToLower();

            _response.Url(x => x.ToLower().Contains(pattern));

            return _response;
        }
    }
}