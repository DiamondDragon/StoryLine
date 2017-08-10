using System;
using StoryLine.Contracts;
using StoryLine.Exceptions;

namespace StoryLine.Actions
{
    internal sealed class LazyAction<TActionBuilder, TArtifact1, TArtifact2, TArtifact3> : IAction, IScenarioEventHandler
        where TActionBuilder : IActionBuilder, new()
    {
        private readonly Action<TActionBuilder, TArtifact1, TArtifact2, TArtifact3> _configator;
        private readonly Func<TArtifact1, bool> _artifact1Filter;
        private readonly Func<TArtifact2, bool> _artifact2Filter;
        private readonly Func<TArtifact3, bool> _artifact3Filter;
        private IAction _action;

        public LazyAction(
            Action<TActionBuilder, TArtifact1, TArtifact2, TArtifact3> configator = null,
            Func<TArtifact1, bool> artifact1Filter = null, 
            Func<TArtifact2, bool> artifact2Filter = null,
            Func<TArtifact3, bool> artifact3Filter = null
        )
        {
            _configator = configator;
            _artifact1Filter = artifact1Filter;
            _artifact2Filter = artifact2Filter;
            _artifact3Filter = artifact3Filter;
        }

        public void Execute(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            var artifact1 = actor.Artifacts.Get(_artifact1Filter);
            if (artifact1 == null)
                throw new ExpectationException($"Expected artifact of type \"{typeof(TArtifact1)}\" was not found.");

            var artifact2 = actor.Artifacts.Get(_artifact2Filter);
            if (artifact2 == null)
                throw new ExpectationException($"Expected artifact of type \"{typeof(TArtifact2)}\" was not found.");

            var artifact3 = actor.Artifacts.Get(_artifact3Filter);
            if (artifact3 == null)
                throw new ExpectationException($"Expected artifact of type \"{typeof(TArtifact3)}\" was not found.");

            var builder = new TActionBuilder();

            _configator?.Invoke(builder, artifact1, artifact2, artifact3);

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