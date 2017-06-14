namespace StoryLine.Rest.Services
{
    public interface IResponseToTextConverter
    {
        string GetText(IResponse response);
    }
}