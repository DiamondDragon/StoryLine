using System;
using StoryLine.Contracts;

namespace StoryLine.Services
{
    public class ScenarioAction : IScenarioAction
    {
        private readonly IActor _actor;
        private readonly IAction _action;

        public ScenarioAction(IActor actor, IAction action)
        {
            _actor = actor ?? throw new ArgumentNullException(nameof(actor));
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Execute()
        {
            _action.Execute(_actor);
        }
    }
}