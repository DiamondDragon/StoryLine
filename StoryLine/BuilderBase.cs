using System;
using StoryLine.Contracts;

namespace StoryLine
{
    public abstract class BuilderBase
    {
        public IScenarioContext Context { get; }

        protected BuilderBase(IScenarioContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}