namespace StoryLine.Wiremock.Services
{
    public interface ITimes
    {
        string Description { get; }
        bool Evaluate(int count);
    }
}
