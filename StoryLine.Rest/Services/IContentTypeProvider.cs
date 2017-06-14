using System.Collections.Generic;

namespace StoryLine.Rest.Services
{
    public interface IContentTypeProvider
    {
        string GetContentType(string contentTypeMediaType);
        string GetContentType(IEnumerable<KeyValuePair<string, string[]>> headers);
        string GetCharSet(IEnumerable<KeyValuePair<string, string[]>> headers);
    }
}