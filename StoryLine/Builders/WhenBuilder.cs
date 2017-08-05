using System;
using StoryLine.Actions;
using StoryLine.Contracts;

namespace StoryLine.Builders
{
    public class WhenBuilder : BuilderBase
    {
        public WhenBuilder(IScenarioContext context)
            : base(context)
        {
        }

        public WhenBuilder Performs<T>(Action<T> config = null)
            where T : IActionBuilder, new()
        {
            var builder = new T();

            config?.Invoke(builder);

            Context.AddAction(builder.Build());

            return this;
        }

        public WhenBuilder Performs<TBuilder, TArtifact1>(
            Action<TBuilder, TArtifact1> config = null,
            Func<TArtifact1, bool> predicate1 = null)
            where TBuilder : IActionBuilder, new()
        {
            Context.AddAction(new LazyAction<TBuilder, TArtifact1>(config, predicate1));

            return this;
        }

        public WhenBuilder Performs<TBuilder, TArtifact1, TArtifact2>(
            Action<TBuilder, TArtifact1, TArtifact2> config = null,
            Func<TArtifact1, bool> predicate1 = null,
            Func<TArtifact2, bool> predicate2 = null
        )
            where TBuilder : IActionBuilder, new()
        {
            Context.AddAction(new LazyAction<TBuilder, TArtifact1, TArtifact2>(config, predicate1, predicate2));

            return this;
        }

        public WhenBuilder Performs<TBuilder, TArtifact1, TArtifact2, TArtifact3>(
            Action<TBuilder, TArtifact1, TArtifact2, TArtifact3> config = null,
            Func<TArtifact1, bool> predicate1 = null,
            Func<TArtifact2, bool> predicate2 = null,
            Func<TArtifact3, bool> predicate3 = null
        )
            where TBuilder : IActionBuilder, new()
        {
            Context.AddAction(new LazyAction<TBuilder, TArtifact1, TArtifact2, TArtifact3>(config, predicate1, predicate2, predicate3));

            return this;
        }

        public ThenBuilder Then()
        {
            return Then(Context.CurrentActor);
        }

        public ThenBuilder Then(IActor actor)
        {
            Context.CurrentActor = actor ?? throw new ArgumentNullException(nameof(actor));

            return new ThenBuilder(Context);
        }

        public void Run(int attemptsCount = 1, int millisecondsTimeout = 1000, bool cleanActorsOnRetry = true)
        {
            if (attemptsCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(attemptsCount));
            if (millisecondsTimeout <= 0)
                throw new ArgumentOutOfRangeException(nameof(millisecondsTimeout));

            Config.CreateScenarioRunner().Run(Context, attemptsCount, millisecondsTimeout, cleanActorsOnRetry);
        }
    }
}