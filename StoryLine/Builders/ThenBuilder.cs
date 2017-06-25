using System;
using StoryLine.Contracts;

namespace StoryLine.Builders
{
    public class ThenBuilder : BuilderBase
    {
        public ThenBuilder(IScenarioContext context)
            : base(context)
        {
        }

        public ThenBuilder Expects<T>(Action<T> config = null)
            where T : IExpectationBuilder, new()
        {
            var builder = new T();

            config?.Invoke(builder);

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