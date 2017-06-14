using System;
using StoryLine.Wiremock.Contracts;
using StoryLine.Wiremock.Services;

namespace StoryLine.Wiremock.Builders
{
    public class ApiStubState : IApiStubState
    {
        private ITimes _requestCount = new Times(x => x >= 1, "at least once.");
        private Request _requestState = new Request();
        private Response _responseState = new Response();

        public Request RequestState
        {
            get => _requestState;
            set => _requestState = value ?? throw new ArgumentNullException(nameof(value));
        }

        public Response ResponseState
        {
            get => _responseState;
            set => _responseState = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ITimes RequestCount
        {
            get => _requestCount;
            set => _requestCount = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}