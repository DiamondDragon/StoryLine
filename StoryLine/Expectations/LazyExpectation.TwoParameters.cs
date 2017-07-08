﻿using System;
using StoryLine.Contracts;
using StoryLine.Exceptions;

namespace StoryLine.Expectations
{
    public class LazyExpectation<TExpectationBuilder, TArtifact1, TArtifact2> : IExpectation
        where TExpectationBuilder : IExpectationBuilder, new()
    {
        private readonly Action<TExpectationBuilder, TArtifact1, TArtifact2> _configurator;
        private readonly Func<TArtifact1, bool> _artifact1Filter;
        private readonly Func<TArtifact2, bool> _artifact2Filter;

        public LazyExpectation(
            Action<TExpectationBuilder, TArtifact1, TArtifact2> configurator = null,
            Func<TArtifact1, bool> artifact1Filter = null,
            Func<TArtifact2, bool> artifact2Filter = null
        )
        {
            _configurator = configurator;
            _artifact1Filter = artifact1Filter;
            _artifact2Filter = artifact2Filter;
        }

        public void Validate(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            var artifact1 = actor.Artifacts.Get(_artifact1Filter);
            if (artifact1 == null)
                throw new ExpectationException($"Expected artifact of type \"{typeof(TArtifact1)}\" was not found.");

            var artifact2 = actor.Artifacts.Get(_artifact2Filter);
            if (artifact2 == null)
                throw new ExpectationException($"Expected artifact of type \"{typeof(TArtifact2)}\" was not found.");

            var builder = new TExpectationBuilder();

            _configurator?.Invoke(builder, artifact1, artifact2);

            builder.Build().Validate(actor);
        }
    }
}