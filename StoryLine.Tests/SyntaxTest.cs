using System;
using FluentAssertions;
using StoryLine.Contracts;
using Xunit;

namespace StoryLine.Tests
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Action1 : IAction
    {
        public void Execute(IActor actor)
        {
            actor.Artifacts.Add(new User
            {
                FirstName = "Dragon",
                LastName = "Diamong"
            });
        }
    }

    public class Builder1 : IActionBuilder
    {
        public IAction Build()
        {
            return new Action1();
        }
    }

    public class Action2 : IAction
    {
        public void Execute(IActor actor)
        {
        }
    }

    public class Builder2 : IActionBuilder
    {
        private User _user;

        public Builder2 SetUser(User user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));

            return this;
        }

        public IAction Build()
        {
            return new Action2();
        }
    }

    public class Expectation1 : IExpectation
    {
        public void Validate(IActor actor)
        {
        }
    }


    public class ExpectationBuilder1 : IExpectationBuilder
    {
        public IExpectation Build()
        {
            return new Expectation1();
        }
    }

    public class SyntaxTest 
    {
        [Fact]
        public void Test()
        {
            Scenario.New()
                .Given()
                    .HasPerformed<Builder1>()
                    .HasPerformed<Builder2, User>((x, y) => 
                        x.SetUser(y))
                .When()
                    .Performs<Builder1>(x => { })
                    .Performs<Builder2>(x => { })
                .Then()
                    .Expects<ExpectationBuilder1>()
                .Run();

        }
    }
}
