namespace StoryLine.Rest.Expectations
{
    public interface IStringContentComparer
    {
        void Verify(string expected, string actual);
    }
}