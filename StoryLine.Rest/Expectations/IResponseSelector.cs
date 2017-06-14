using StoryLine.Rest.Services;

namespace StoryLine.Rest.Expectations
{
    public interface IResponseSelector
    {
        bool Maches(IResponse response);
    }
}