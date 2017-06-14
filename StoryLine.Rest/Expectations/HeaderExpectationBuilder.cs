using System;
using System.Text.RegularExpressions;

namespace StoryLine.Rest.Expectations
{
    public class HeaderExpectationBuilder
    {
        private readonly string _header;
        private readonly HttpResponse _builder;

        public HeaderExpectationBuilder(string header, HttpResponse builder)
        {
            if (string.IsNullOrWhiteSpace(header))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(header));

            _header = header;
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public HttpResponse EqualTo(string value)
        {
            return EqualTo(value, StringComparison.OrdinalIgnoreCase);
        }

        public HttpResponse EqualTo(string value, StringComparison comparison)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            _builder.RequestExpectation(new ResponseHeaderExpectation(
                _header,
                x => x.Equals(value, comparison),
                x => $"Expected value must be equal to \"{value}\", actual values were \"{x}\"."
            ));

            return _builder;
        }

        public HttpResponse Match(string value, RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            _builder.RequestExpectation(new ResponseHeaderExpectation(
                _header,
                x => Regex.IsMatch(x, value, options),
                x => $"Expected value must match regual expression \"{value}\", actual values were \"{x}\"."
            ));

            return _builder;
        }

        public HttpResponse Start(string value)
        {
            return Start(value, StringComparison.OrdinalIgnoreCase);
        }

        public HttpResponse Start(string value, StringComparison comparison)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            var lowerCasePattern = value.ToLower();

            _builder.RequestExpectation(new ResponseHeaderExpectation(
                _header,
                x => x.StartsWith(lowerCasePattern, comparison),
                x => $"Expected value must start \"{value}\", actual values were \"{x}\"."
            ));

            return _builder;
        }

        public HttpResponse Contain(string value, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            var lowerCasePattern = ignoreCase ? value.ToLower() : value;

            _builder.RequestExpectation(new ResponseHeaderExpectation(
                _header,
                x => (ignoreCase ? x.ToLower() : x).Contains(lowerCasePattern),
                x => $"Expected value must contain \"{value}\", actual values were \"{x}\"."
            ));

            return _builder;
        }
    }
}