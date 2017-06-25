using System;
using System.Collections.Generic;
using System.Text;
using StoryLine.Contracts;
using StoryLine.Exceptions;

namespace StoryLine.Expectations
{
    public sealed class PredicateArtifactExpectation<TArtifact> : ArtifactExpectationBase<TArtifact>
    {
        private readonly Func<TArtifact, bool> _predicate;

        public PredicateArtifactExpectation(
            Func<TArtifact, bool> predicate,
            Func<TArtifact, bool> artifactFilter = null) 
            : base(artifactFilter)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        protected override void OnValidate(TArtifact artifact)
        {
            if (!_predicate(artifact))
                throw new ExpectationException($"Artifact of type \"{typeof(TArtifact)}\" doesn't match expectation.");
        }
    }
}
