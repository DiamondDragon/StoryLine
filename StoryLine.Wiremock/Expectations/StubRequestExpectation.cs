using System;
using StoryLine.Contracts;
using StoryLine.Exceptions;
using StoryLine.Wiremock.Builders;

namespace StoryLine.Wiremock.Expectations
{
    public class StubRequestExpectation : IExpectation
    {
        private readonly IApiStubState _state;

        public StubRequestExpectation(IApiStubState state)
        {
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public void Validate(IActor actor)
        {
            
            var count = Config.Client.Count(_state.RequestState);

            if (!_state.RequestCount.Evaluate(count))
                throw new ExpectationException($"Expected Api to be called '{_state.RequestCount.Description}' but was called '{count}'");
        }
    }
}