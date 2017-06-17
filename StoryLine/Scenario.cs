using StoryLine.Builders;
using StoryLine.Services;

namespace StoryLine
{
    public class Scenario
    {
        public static ScenarioBuilder New()
        {
            return new ScenarioBuilder(new ScenarioContext());
        }
    }
}
