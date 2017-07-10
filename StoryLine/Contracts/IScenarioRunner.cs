namespace StoryLine.Contracts
{
    public interface IScenarioRunner
    {
        void Run(IScenarioContext context, int attemptsCount, int millisecondsTimeout, bool cleanActorsOnRetry);
    }
}