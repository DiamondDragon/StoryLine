using System;

namespace StoryLine.Exceptions
{
    public class ExpectationException : Exception
    {
        public ExpectationException()
        {
        }

        public ExpectationException(string message)
            : base(message)
        {
        }

        public ExpectationException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
