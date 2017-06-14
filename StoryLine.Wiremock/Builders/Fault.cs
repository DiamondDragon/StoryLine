using System;

namespace StoryLine.Wiremock.Builders
{
    public class Fault
    {
        /// <summary>
        /// Return a completely empty response.
        /// </summary>
        public static Fault EmptyResponse => new Fault("EMPTY_RESPONSE");

        /// <summary>
        /// Send an OK status header, then garbage, then close the connection.
        /// </summary>
        public static Fault MalformedResponseChunk => new Fault("MALFORMED_RESPONSE_CHUNK");

        /// <summary>
        /// Send garbage then close the connection.
        /// </summary>
        public static Fault RandomDataThenClose => new Fault("RANDOM_DATA_THEN_CLOSE");

        public string Value { get; }

        public Fault(string fault)
        {
            if (string.IsNullOrEmpty(fault))
                throw new ArgumentNullException(nameof(fault));

            Value = fault;
        }
    }
}