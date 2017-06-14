using System.Collections.Generic;

namespace StoryLine.Wiremock.Contracts
{
    public class Response
    {
        public int Status { get; set; }
        public string Body { get; set; }
        public string Base64Body { get; set; }
        public int FixedDelayMilliseconds { get; set; }
        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        public string Fault { get; set; }
    }
}