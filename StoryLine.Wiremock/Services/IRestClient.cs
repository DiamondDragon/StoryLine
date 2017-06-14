namespace StoryLine.Wiremock.Services
{
    public interface IRestClient
    {
        void PostJson(string url);
        TResult PostJson<TResult>(string url, object body);
        void Delete(string url);
    }
}