using System;
using System.Text.RegularExpressions;

namespace StoryLine.Rest.Expectations
{
    public class TextBodyExpectationBuilder
    {
        private readonly HttpResponse _builder;

        public TextBodyExpectationBuilder(HttpResponse builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public PlainTextResourceBodyExpectationBuilder EqualTo()
        {
            return new PlainTextResourceBodyExpectationBuilder(_builder);
        }

        public HttpResponse EqualTo(string value)
        {
            return EqualTo(value, StringComparison.OrdinalIgnoreCase);
        }

        public HttpResponse EqualTo(string value, StringComparison comparison)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            _builder.RequestExpectation(new ResponseTextBodyExpectation(
                x => x.Equals(value, comparison),
                x => $"Expected value must be equal to \"{value}\", but actual value was \"{x}\"."
            ));

            return _builder;
        }

        public HttpResponse Matching(string value, RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            _builder.RequestExpectation(new ResponseTextBodyExpectation(
                x => Regex.IsMatch(x, value, options),
                x => $"Expected value must match regual expression \"{value}\", but actual value was \"{x}\"."
            ));

            return _builder;
        }

        public HttpResponse StartingWith(string value)
        {
            return StartingWith(value, StringComparison.OrdinalIgnoreCase);
        }

        public HttpResponse StartingWith(string value, StringComparison comparison)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            var lowerCasePattern = value.ToLower();

            _builder.RequestExpectation(new ResponseTextBodyExpectation(
                x => x.StartsWith(lowerCasePattern, comparison),
                x => $"Expected value must start \"{value}\", but actual value was \"{x}\"."
            ));

            return _builder;
        }

        public HttpResponse Containing(string value, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            var lowerCasePattern = ignoreCase ? value.ToLower() : value;

            _builder.RequestExpectation(new ResponseTextBodyExpectation(
                x => (ignoreCase ? x.ToLower() : x).Contains(lowerCasePattern),
                x => $"Expected value must contain \"{value}\", but actual value was \"{x}\"."
            ));

            return _builder;
        }
    }
}