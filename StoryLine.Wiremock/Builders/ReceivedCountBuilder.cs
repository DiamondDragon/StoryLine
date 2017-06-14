using System;
using StoryLine.Wiremock.Services;

namespace StoryLine.Wiremock.Builders
{
    public class ReceivedCountBuilder
    {
        private readonly IApiStubState _state;

        public ReceivedCountBuilder(IApiStubState state)
        {
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public ReceivedCountBuilder Never()
        {
            return SetRequestCount(count => count == 0, "never");
        }
        public ReceivedCountBuilder Once()
        {
            return SetRequestCount(count => count == 1, "once");
        }

        public ReceivedCountBuilder Twice()
        {
            return SetRequestCount(count => count == 2, "twice");
        }

        public ReceivedCountBuilder AtLeastOnce()
        {
            return SetRequestCount(count => count >= 1, "at least once");
        }

        public ReceivedCountBuilder MoreThanOnce()
        {
            return SetRequestCount(count => count > 1, "more than once");
        }

        public ReceivedCountBuilder Exactly(int number)
        {
            if (number <= 0)
                throw new ArgumentOutOfRangeException(nameof(number));

            return SetRequestCount(count => count == number, string.Format("{0} times", number));
        }

        private ReceivedCountBuilder SetRequestCount(Predicate<int> predicate, string description)
        {
            _state.RequestCount = new Times(predicate, description);

            return this;
        }
    }
}