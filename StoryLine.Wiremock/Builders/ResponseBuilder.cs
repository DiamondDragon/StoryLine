using System;
using System.Net;

namespace StoryLine.Wiremock.Builders
{
    public class ResponseBuilder : StubBuilderBase
    {
        public ResponseBuilder(IApiStubState state) 
            : base(state)
        {
        }

        public ResponseBuilder Status(int status)
        {
            State.ResponseState.Status = status;
            return new ResponseBuilder(State);
        }

        /// <summary>
        /// Set status by using HttpStatusCode
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public ResponseBuilder Status(HttpStatusCode status)
        {
            State.ResponseState.Status = (int)status;
            return new ResponseBuilder(State);
        }

        public ResponseBuilder Body(string body)
        {
            State.ResponseState.Body = body ?? throw new ArgumentNullException(nameof(body));

            return this;
        }

        public ResponseBuilder BinaryBody(byte[] content)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            State.ResponseState.Base64Body = Convert.ToBase64String(content);

            return this;
        }

        public ResponseBuilder Header(string key, string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));

            State.ResponseState.Headers.Add(key, value);

            return this;
        }

        public ResponseBuilder FixedDelay(int milliseconds)
        {
            if (milliseconds <= 0)
                throw new ArgumentOutOfRangeException(nameof(milliseconds));

            State.ResponseState.FixedDelayMilliseconds = milliseconds;

            return this;
        }

        public ResponseBuilder Fault(Fault fault)
        {
            if (fault == null)
                throw new ArgumentNullException(nameof(fault));

            State.ResponseState.Fault = fault.Value;
            return this;
        }
    }
}