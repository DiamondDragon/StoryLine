using System;
using System.Collections.Generic;

namespace StoryLine.Rest.Services
{
    public interface IResponse
    {
        IRequest Request { get; }
        Exception Exception { get; }
        IReadOnlyDictionary<string, string[]> Headers { get; }
        byte[] Body { get; }
        int Status { get; }
        string ReasonPhrase { get; }
    }
}