
using System;

namespace StoryLine.Wiremock.Builders
{
    public abstract class StubBuilderBase
    {
        protected IApiStubState State { get; }

        protected StubBuilderBase(IApiStubState state)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
        }
    }
}