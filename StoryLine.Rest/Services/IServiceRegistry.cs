namespace StoryLine.Rest.Services
{
    public interface IServiceRegistry
    {
        IServiceConfig GetConfig(string serviceName);
    }
}