namespace StoryLine.Wiremock.Services
{
    public interface IServiceRegistry
    {
        T Get<T>();
    }
}