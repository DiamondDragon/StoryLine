using System;
using StoryLine.Contracts;
using StoryLine.Exceptions;

namespace StoryLine.Actions
{
    internal sealed class LazyAction<TActionBuilder, TArtifact1> : IAction, IScenarioEventHandler
        where TActionBuilder : IActionBuilder, new()
    {
        private readonly Action<TActionBuilder, TArtifact1> _configator;
        private readonly Func<TArtifact1, bool> _artifact1Filter;
        private IAction _action;

        public LazyAction(
            Action<TActionBuilder, TArtifact1> configator = null, 
            Func<TArtifact1, bool> artifact1Filter = null)
        {
            _configator = configator;
            _artifact1Filter = artifact1Filter;
        }

        public void Execute(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            var artifact = actor.Artifacts.Get(_artifact1Filter);

            if (artifact == null)
                throw new ExpectationException($"Expected artifact of type \"{typeof(TArtifact1)}\" was not found.");

            var builder = new TActionBuilder();

            _configator?.Invoke(builder, artifact);

            _action = builder.Build();
            _action.Execute(actor);
        }

        public void OnExecuted(ScenarioExecutedEventArgs args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            (_action as IScenarioEventHandler)?.OnExecuted(args);
        }
    }
}
