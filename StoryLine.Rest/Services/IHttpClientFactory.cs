namespace StoryLine.Rest.Services
{
    public interface IHttpClientFactory
    {
        IHttpClient Create(string service);
    }
}