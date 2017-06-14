using System;
using StoryLine.Contracts;
using StoryLine.Services;

namespace StoryLine
{
    public static class Config
    {
        public static Func<IScenarioRunner> CreateScenarioRunner = () => new ScenarioRunner();
    }
}