using System;
using System.Threading;
using StoryLine.Contracts;
using StoryLine.Exceptions;

namespace StoryLine.Services
{
    public class ScenarioRunner : IScenarioRunner
    {
        public void Run(IScenarioContext context, int attemptsCount, int millisecondsTimeout)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (attemptsCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(attemptsCount));
            if (millisecondsTimeout <= 0)
                throw new ArgumentOutOfRangeException(nameof(millisecondsTimeout));

            for (var i = 0; i < attemptsCount; i++)
            {
                try
                {
                    RunScenario(context);
                    break;
                }
                catch (ExpectationException)
                {
                    Thread.Sleep(millisecondsTimeout);
                }
            }
        }

        private static void RunScenario(IScenarioContext context)
        {
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