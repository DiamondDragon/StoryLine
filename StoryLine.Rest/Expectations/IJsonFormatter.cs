namespace StoryLine.Rest.Expectations
{
    public interface IJsonFormatter
    {
        string Format(string content, params string[] propertiesToIgnore);
    }
}