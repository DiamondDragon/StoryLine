using StoryLine.Contracts;
using Xunit;

namespace StoryLine.Tests
{
    public class User
    {
        
    }

    public class Action1 : IActionBuilder
    {
        public IAction Build()
        {
            return null;
        }
    }

    public class Action2 : IActionBuilder
    {
        public IAction Build()
        {
            return null;
        }
    }

    public class Expectation1 : IExpectationBuilder
    {
        public IExpectation Build()
        {
            return null;
        }
    }

    public class SyntaxTest 
    {
        [Fact]
        public void Test()
        {
            Scenario.New()
                .Given()
                    .HasPerformed<Action1>()
                    .HasPerformed<Action2>(x => { })
                .When()
                    .Performs<Action1>(x => { })
                    .Performs<Action2>(x => { })
                .Then()
                    .Expects<Expectation1>(x => { })
                .Run();

        }
    }
}
