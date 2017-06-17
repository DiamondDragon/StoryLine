using System;
using StoryLine.Contracts;
using StoryLine.Services;

namespace StoryLine
{
    public static class Config
    {
        private static Func<IScenarioRunner> _createScenarioRunner = () => new ScenarioRunner();

        public static Func<IScenarioRunner> CreateScenarioRunner
        {
            get => _createScenarioRunner;
            set => _createScenarioRunner = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}