namespace StoryLine.Contracts
{
    public interface IScenarioExpectation
    {
        IActor Actor { get; }
        IExpectation Expectation { get; }

        void Validate();
    }
}