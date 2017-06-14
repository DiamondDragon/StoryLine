using System.Collections.Generic;

namespace StoryLine.Rest.Services
{
    public interface IRequest
    {
        string Service { get; set; }
        string Method { get; set; }
        string Url { get; set; }
        IReadOnlyDictionary<string, string[]> Headers { get; }
        byte[] Body { get; set; }
    }
}