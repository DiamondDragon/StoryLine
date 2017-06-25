using System;
using StoryLine.Contracts;
using StoryLine.Exceptions;

namespace StoryLine.Actions
{
    public sealed class LazyAction<TActionBuilder, TArtifact1, TArtifact2> : IAction
        where TActionBuilder : IActionBuilder, new()
    {
        private readonly Action<TActionBuilder, TArtifact1, TArtifact2> _configator;
        private readonly Func<TArtifact1, bool> _predicate1;
        private readonly Func<TArtifact2, bool> _predicate2;

        public LazyAction(Action<TActionBuilder, TArtifact1, TArtifact2> configator = null, Func<TArtifact1, bool> predicate1 = null, Func<TArtifact2, bool> predicate2 = null)
        {
            _configator = configator;
            _predicate1 = predicate1;
            _predicate2 = predicate2;
        }

        public void Execute(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            var artifact1 = actor.Artifacts.Get(_predicate1);
            if (artifact1 == null)
                throw new ExpectationException($"Expected artifact of type \"{nameof(TArtifact1)}\" was not found.");

            var artifact2 = actor.Artifacts.Get(_predicate2);
            if (artifact2 == null)
                throw new ExpectationException($"Expected artifact of type \"{nameof(TArtifact2)}\" was not found.");


            var builder = new TActionBuilder();

            _configator?.Invoke(builder, artifact1, artifact2);

            builder.Build().Execute(actor);
        }
    }
}