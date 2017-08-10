namespace StoryLine.Contracts
{
    public interface IScenarioEventHandler
    {
        void OnExecuted(ScenarioExecutedEventArgs args);
    }
}
