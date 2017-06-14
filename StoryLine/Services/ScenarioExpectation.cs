using System;
using StoryLine.Contracts;

namespace StoryLine.Services
{
    public class ScenarioExpectation : IScenarioExpectation
    {
        private readonly IActor _actor;
        private readonly IExpectation _expectation;

        public ScenarioExpectation(IActor actor, IExpectation expectation)
        {
            _actor = actor ?? throw new ArgumentNullException(nameof(actor));
            _expectation = expectation ?? throw new ArgumentNullException(nameof(expectation));
        }

        public void Validate()
        {
            _expectation.Validate(_actor);
        }
    }
}