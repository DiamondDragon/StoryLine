using System;
using StoryLine.Contracts;
using StoryLine.Exceptions;

namespace StoryLine.Actions
{
    public sealed class LazyAction<TActionBuilder, TArtifact1> : IAction
        where TActionBuilder : IActionBuilder, new()
    {
        private readonly Action<TActionBuilder, TArtifact1> _configator;
        private readonly Func<TArtifact1, bool> _artifact1Filter;

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

            builder.Build().Execute(actor);
        }
    }
}
