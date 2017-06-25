using System;
using StoryLine.Contracts;
using StoryLine.Expectations;

namespace StoryLine.Builders
{
    public class ThenBuilder : BuilderBase
    {
        public ThenBuilder(IScenarioContext context)
            : base(context)
        {
        }

        public ThenBuilder Expects<TBuilder>(Action<TBuilder> config = null)
            where TBuilder : IExpectationBuilder, new()
        {
            var builder = new TBuilder();

            config?.Invoke(builder);

            Context.AddExpectation(builder.Build());

            return this;
        }

        public ThenBuilder ExpectsArtifact<TArtifact>(Action<TArtifact> validator, Func<TArtifact, bool> filter = null)
        {
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            Context.AddExpectation(new ValidatorArtifactExpectation<TArtifact>(validator, filter));

            return this;
        }

        public ThenBuilder ExpectsArtifact<TArtifact>(Func<TArtifact, bool> predicate, Func<TArtifact, bool> filter = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            Context.AddExpectation(new PredicateArtifactExpectation<TArtifact>(predicate, filter));

            return this;
        }

        public ThenBuilder Then(IActor actor)
        {
            Context.CurrentActor = actor ?? throw new ArgumentNullException(nameof(actor));

            return this;
        }

        public void Run()
        {
            Config.CreateScenarioRunner().Run(Context);
        }
    }
}