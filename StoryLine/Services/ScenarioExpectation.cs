using System;
using StoryLine.Contracts;

namespace StoryLine.Services
{
    public sealed class ScenarioExpectation : IScenarioExpectation
    {
        public IActor Actor { get; }
        public IExpectation Expectation { get; }

        public ScenarioExpectation(IActor actor, IExpectation expectation)
        {
            Actor = actor ?? throw new ArgumentNullException(nameof(actor));
            Expectation = expectation ?? throw new ArgumentNullException(nameof(expectation));
        }

        public void Validate()
        {
            Expectation.Validate(Actor);
        }
    }
}