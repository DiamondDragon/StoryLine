using System;
using StoryLine.Exceptions;
using StoryLine.Rest.Services;

namespace StoryLine.Rest.Expectations
{
    public class ResponseHeaderExpectation : IResponseExpectation
    {
        private readonly string _header;
        private readonly Func<string, bool> _valueValidator;
        private readonly Func<string, string> _message;

        public ResponseHeaderExpectation(string header, Func<string, bool> valueValidator, Func<string, string> message)
        {
            if (string.IsNullOrWhiteSpace(header))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(header));

            _header = header;
            _valueValidator = valueValidator ?? throw new ArgumentNullException(nameof(valueValidator));
            _message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public void Validate(IResponse response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (!response.Headers.ContainsKey(_header))
                throw new ExpectationException($"Expected header \"{_header}\" was not found in response.");

            var values = response.Headers[_header];

            foreach (var value in values)
            {
                if (_valueValidator(value))
                    return;
            }

            throw new ExpectationException(_message(string.Join(", ", values)));
        }
    }
}