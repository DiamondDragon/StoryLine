namespace StoryLine.Rest.Expectations
{
    public interface ITextVerifier
    {
        void Verify(string expectedValue, string actualValue);
    }
}