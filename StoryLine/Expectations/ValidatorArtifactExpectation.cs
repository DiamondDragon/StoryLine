using System;
using StoryLine.Exceptions;

namespace StoryLine.Expectations
{
    public sealed class ValidatorArtifactExpectation<TArtifact> : ArtifactExpectationBase<TArtifact>
    {
        private readonly Action<TArtifact> _validator;

        public ValidatorArtifactExpectation(
            Action<TArtifact> validator,
            Func<TArtifact, bool> artifactFilter = null) 
            : base(artifactFilter)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected override void OnValidate(TArtifact artifact)
        {
            try
            {
                _validator(artifact);
            }
            catch (Exception ex)
            {
                throw new ExpectationException(ex.Message, ex);
            }
        }
    }
}