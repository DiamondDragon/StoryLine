namespace StoryLine.Rest.Services
{
    public interface IRestClient
    {
        IResponse Send(IRequest request);
    }
}