using System;
using System.Collections.Generic;

namespace StoryLine.Rest.Services
{
    public class Response : IResponse
    {
        public IRequest Request { get; set; }
        public Exception Exception { get; set; }
        public IReadOnlyDictionary<string, string[]> Headers { get; set; } = new Dictionary<string, string[]>();
        public byte[] Body { get; set; }
        public int Status { get; set; }
        public string ReasonPhrase { get; set; }
    }
}