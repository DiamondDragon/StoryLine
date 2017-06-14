namespace StoryLine.Contracts
{
    public interface IAction
    {
        void Execute(IActor actor);
    }
}