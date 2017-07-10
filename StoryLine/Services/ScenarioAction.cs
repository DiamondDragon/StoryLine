using System;
using StoryLine.Contracts;

namespace StoryLine.Services
{
    public sealed class ScenarioAction : IScenarioAction
    {
        public IActor Actor { get; }
        public IAction Action { get; }

        public ScenarioAction(IActor actor, IAction action)
        {
            Actor = actor ?? throw new ArgumentNullException(nameof(actor));
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Execute()
        {
            Action.Execute(Actor);
        }
    }
}