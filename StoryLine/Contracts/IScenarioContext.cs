using System.Collections.Generic;

namespace StoryLine.Contracts
{
    public interface IScenarioContext
    {
        IActor CurrentActor { get; set; }
        IEnumerable<IScenarioAction> Actions { get; }
        IEnumerable<IScenarioExpectation> Expectations { get; }

        void AddAction(IAction action);
        void AddExpectation(IExpectation expectation);
    }
}