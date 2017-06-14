using System;
using StoryLine.Exceptions;
using StoryLine.Rest.Services;

namespace StoryLine.Rest.Expectations
{
    public class ResponseTextBodyExpectation : IResponseExpectation
    {
        private readonly Func<string, bool> _predicate;
        private readonly Func<string, string> _message;

        public ResponseTextBodyExpectation(Func<string, bool> predicate, Func<string, string> message)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            _message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public void Validate(IResponse response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            var text = Config.ResponseToTextConverter.GetText(response);

            if (!_predicate(text))
                throw new ExpectationException(_message(text));
        }
    }
}