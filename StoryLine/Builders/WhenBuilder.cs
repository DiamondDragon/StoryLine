using System;
using StoryLine.Contracts;

namespace StoryLine.Builders
{
    public class WhenBuilder : BuilderBase
    {
        public WhenBuilder(IScenarioContext context)
            : base(context)
        {
        }

        public WhenBuilder Performs<T>(Action<T> config = null)
            where T : IActionBuilder, new()
        {
            var builder = new T();

            config?.Invoke(builder);

            Context.AddAction(builder.Build());

            return this;
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