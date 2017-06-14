using System;
using System.Collections.Generic;
using StoryLine.Contracts;

namespace StoryLine.Services
{
    public class ScenarioContext : IScenarioContext
    {
        private readonly List<IScenarioAction> _actions = new List<IScenarioAction>();
        private readonly List<IScenarioExpectation> _expectations = new List<IScenarioExpectation>();

        public IActor CurrentActor { get; set; } = new Actor();

        public IEnumerable<IScenarioAction> Actions => _actions;
        public IEnumerable<IScenarioExpectation> Expectations => _expectations;

        public void AddAction(IAction action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            _actions.Add(new ScenarioAction(CurrentActor, action));
        }

        public void AddExpectation(IExpectation expectation)
        {
            if (expectation == null)
                throw new ArgumentNullException(nameof(expectation));

            _expectations.Add(new ScenarioExpectation(CurrentActor, expectation));
        }
    }
}