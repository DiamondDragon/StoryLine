using System.Collections.Generic;

namespace StoryLine.Rest.Services
{
    public class Request : IRequest
    {
        public string Service { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public IReadOnlyDictionary<string, string[]> Headers { get; set; } = new Dictionary<string, string[]>();
        public byte[] Body { get; set; }
    }
}