using System;

namespace StoryLine.Contracts
{
    public class ScenarioExecutedEventArgs
    {
        public Exception Exception { get; }
        public bool Success => Exception == null;

        public ScenarioExecutedEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}