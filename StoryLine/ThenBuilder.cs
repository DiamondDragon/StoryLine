using System;
using StoryLine.Contracts;

namespace StoryLine
{
    public class ThenBuilder : BuilderBase
    {
        public ThenBuilder(IScenarioContext context)
            : base(context)
        {
        }

        public ThenBuilder Expects<T>(Action<T> config)
            where T : IExpectationBuilder, new()
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var builder = new T();

            config(builder);

            Context.AddExpectation(builder.Build());

            return this;
        }

        public ThenBuilder Then(IActor actor)
        {
            Context.CurrentActor = actor ?? throw new ArgumentNullException(nameof(actor));

            return this;
        }

        public void Run()
        {
            Config.CreateScenarioRunner().Run(Context);
        }
    }
}