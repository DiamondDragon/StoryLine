using System;
using StoryLine.Contracts;

namespace StoryLine
{
    public class WhenBuilder : BuilderBase
    {
        public WhenBuilder(IScenarioContext context)
            : base(context)
        {
        }

        public IntentionBuilder Performs<T>(Action<T> config)
            where T : IActionBuilder, new()
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var builder = new T();

            config(builder);

            Context.AddAction(builder.Build());

            return new IntentionBuilder(Context);
        }
    }
}