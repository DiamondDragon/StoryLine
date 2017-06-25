using System;
using StoryLine.Actions;
using StoryLine.Contracts;

namespace StoryLine.Builders
{
    public class GivenBuilder : BuilderBase
    {
        public GivenBuilder(IScenarioContext context) 
            : base(context)
        {
        }

        public GivenBuilder HasPerformed<TBuilder>(Action<TBuilder> config = null)
            where TBuilder : IActionBuilder, new()
        {
            var builder = new TBuilder();

            config?.Invoke(builder);

            Context.AddAction(builder.Build());

            return this;
        }

        public GivenBuilder HasPerformed<TBuilder, TArtifact1>(
            Action<TBuilder, TArtifact1> config = null, 
            Func<TArtifact1, bool> predicate1 = null)
            where TBuilder : IActionBuilder, new()
        {
            Context.AddAction(new DeferredAction<TBuilder, TArtifact1>(config, predicate1));

            return this;
        }

        public GivenBuilder HasPerformed<TBuilder, TArtifact1, TArtifact2>(
            Action<TBuilder, TArtifact1, TArtifact2> config = null,
            Func<TArtifact1, bool> predicate1 = null,
            Func<TArtifact2, bool> predicate2 = null
            )
            where TBuilder : IActionBuilder, new()
        {
            Context.AddAction(new DeferredAction<TBuilder, TArtifact1, TArtifact2>(config, predicate1, predicate2));

            return this;
        }

        public GivenBuilder HasPerformed<TBuilder, TArtifact1, TArtifact2, TArtifact3>(
            Action<TBuilder, TArtifact1, TArtifact2, TArtifact3> config = null,
            Func<TArtifact1, bool> predicate1 = null,
            Func<TArtifact2, bool> predicate2 = null,
            Func<TArtifact3, bool> predicate3 = null
        )
            where TBuilder : IActionBuilder, new()
        {
            Context.AddAction(new DeferredAction<TBuilder, TArtifact1, TArtifact2, TArtifact3>(config, predicate1, predicate2, predicate3));

            return this;
        }

        public GivenBuilder Given(IActor actor)
        {
            Context.CurrentActor = actor ?? throw new ArgumentNullException(nameof(actor));

            return this;
        }

        public WhenBuilder When()
        {
            return When(Context.CurrentActor);
        }

        public WhenBuilder When(IActor actor)
        {
            Context.CurrentActor = actor ?? throw new ArgumentNullException(nameof(actor));

            return new WhenBuilder(Context);
        }
    }
}