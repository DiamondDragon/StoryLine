using System;
using System.Linq;
using System.Threading;
using StoryLine.Contracts;
using StoryLine.Exceptions;

namespace StoryLine.Services
{
    public class ScenarioRunner : IScenarioRunner
    {
        public void Run(IScenarioContext context, int attemptsCount, int millisecondsTimeout, bool cleanActorsOnRetry)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (attemptsCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(attemptsCount));
            if (millisecondsTimeout <= 0)
                throw new ArgumentOutOfRangeException(nameof(millisecondsTimeout));

            for (var i = 0; i < attemptsCount; i++)
            {
                if (cleanActorsOnRetry && i > 0)
                    CleanActors(context);

                try
                {
                    RunScenario(context);
                    break;
                }
                catch (ExpectationException)
                {
                    if (i + 1 < attemptsCount)
                        Thread.Sleep(millisecondsTimeout);
                    else
                        throw;
                }
            }
        }

        private static void CleanActors(IScenarioContext context)
        {
            var actionActors =
                from action in context.Actions
                select action.Actor;

            var expectationActors =
                from expectation in context.Actions
                select expectation.Actor;

            foreach (var actor in actionActors.Concat(expectationActors).Distinct())
            {
                actor.Artifacts.Clear();
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