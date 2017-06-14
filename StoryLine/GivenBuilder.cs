using System;
using StoryLine.Contracts;

namespace StoryLine
{
    public class GivenBuilder : BuilderBase
    {
        public GivenBuilder(IScenarioContext context) 
            : base(context)
        {
        }

        public GivenBuilder HasPerformed<T>(Action<T> config)
            where T : IActionBuilder, new()
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var builder = new T();

            config(builder);

            Context.AddAction(builder.Build());

            return this;
        }

        public GivenBuilder Given(IActor actor)
        {
            Context.CurrentActor = actor ?? throw new ArgumentNullException(nameof(actor));

            return this;
        }

        public WhenBuilder When()
        {
            return When(Context.CurrentActor);
        }

        public WhenBuilder When(IActor actor)
        {
            Context.CurrentActor = actor ?? throw new ArgumentNullException(nameof(actor));

            return new WhenBuilder(Context);
        }
    }
}