using System;
using StoryLine.Contracts;
using StoryLine.Exceptions;
using StoryLine.Rest.Services;

namespace StoryLine.Rest.Expectations
{
    public class HttResponseExpectation : IExpectation
    {
        public IResponseExpectation[] Expectations { get; set; } = new IResponseExpectation[0];
        public IResponseSelector Selector { get; set; }

        public void Validate(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            var response = actor.Artifacts.Get<IResponse>(Selector.Maches);
            if (response == null)
                throw new ExpectationException("Matching response was not found");

            foreach (var expectation in Expectations)
            {
                expectation.Validate(response);
            }
        }
    }
}