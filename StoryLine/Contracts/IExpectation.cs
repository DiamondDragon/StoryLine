namespace StoryLine.Contracts
{
    public interface IExpectation
    {
        void Validate(IActor actor);
    }
}