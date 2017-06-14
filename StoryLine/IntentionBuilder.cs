using System;
using StoryLine.Contracts;

namespace StoryLine
{
    public class IntentionBuilder : BuilderBase
    {
        public IntentionBuilder(IScenarioContext context)
            : base(context)
        {
        }

        public ThenBuilder Then()
        {
            return Then(Context.CurrentActor);
        }

        private ThenBuilder Then(IActor actor)
        {
            Context.CurrentActor = actor ?? throw new ArgumentNullException(nameof(actor));

            return new ThenBuilder(Context);
        }

        public void Run()
        {
            Config.CreateScenarioRunner().Run(Context);
        }
    }
}