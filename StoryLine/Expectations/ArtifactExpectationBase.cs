using System;
using StoryLine.Contracts;
using StoryLine.Exceptions;

namespace StoryLine.Expectations
{
    public abstract class ArtifactExpectationBase<TArtifact> : IExpectation
    {
        private readonly Func<TArtifact, bool> _artifactFilter;

        protected ArtifactExpectationBase(Func<TArtifact, bool> artifactFilter = null)
        {
            _artifactFilter = artifactFilter;
        }

        public void Validate(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            var artifact = actor.Artifacts.Get(_artifactFilter);

            if (artifact == null)
                throw new ExpectationException($"Expected artifact of type \"{typeof(TArtifact)}\" was not found.");

            OnValidate(artifact);
        }

        protected abstract void OnValidate(TArtifact artifact);
    }
}