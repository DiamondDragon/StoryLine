using System;
using StoryLine.Contracts;
using StoryLine.Exceptions;

namespace StoryLine.Expectations
{
    public class LazyExpectation<TExpectationBuilder, TArtifact1> : IExpectation, IScenarioEventHandler
        where TExpectationBuilder : IExpectationBuilder, new()
    {
        private readonly Action<TExpectationBuilder, TArtifact1> _configurator;
        private readonly Func<TArtifact1, bool> _artifact1Filter;
        private IExpectation _expectation;

        public LazyExpectation(
            Action<TExpectationBuilder, TArtifact1> configurator = null,
            Func<TArtifact1, bool> artifact1Filter = null)
        {
            _configurator = configurator;
            _artifact1Filter = artifact1Filter;
        }

        public void Validate(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            var artifact = actor.Artifacts.Get(_artifact1Filter);

            if (artifact == null)
                throw new ExpectationException($"Expected artifact of type \"{typeof(TArtifact1)}\" was not found.");

            var builder = new TExpectationBuilder();

            _configurator?.Invoke(builder, artifact);

            _expectation = builder.Build();
            _expectation.Validate(actor);
        }

        public void OnExecuted(ScenarioExecutedEventArgs args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            (_expectation as IScenarioEventHandler)?.OnExecuted(args);
        }
    }
}
