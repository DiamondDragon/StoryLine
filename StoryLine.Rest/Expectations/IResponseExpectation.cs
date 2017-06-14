using StoryLine.Rest.Services;

namespace StoryLine.Rest.Expectations
{
    public interface IResponseExpectation
    {
        void Validate(IResponse response);
    }
}