using System;
using StoryLine.Contracts;
using StoryLine.Wiremock.Builders;
using StoryLine.Wiremock.Contracts;

namespace StoryLine.Wiremock.Actions
{
    public class StubResponseAction : IAction
    {
        private readonly IApiStubState _state;

        public StubResponseAction(IApiStubState state)
        {
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public void Execute(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            Config.Client.Create(new Mapping
            {
                Request = _state.RequestState,
                Response = _state.ResponseState
            });
        }
    }
}