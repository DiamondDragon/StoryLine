using System;
using StoryLine.Contracts;

namespace StoryLine.Services
{
    public class ScenarioRunner : IScenarioRunner
    {
        public void Run(IScenarioContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            foreach (var step in context.Actions)
            {
                step.Execute();
            }

            foreach (var expectation in context.Expectations)
            {
                expectation.Validate();
            }
        }
    }
}