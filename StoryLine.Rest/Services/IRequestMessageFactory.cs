using System.Net.Http;

namespace StoryLine.Rest.Services
{
    public interface IRequestMessageFactory
    {
        HttpRequestMessage Create(IRequest request);
    }
}