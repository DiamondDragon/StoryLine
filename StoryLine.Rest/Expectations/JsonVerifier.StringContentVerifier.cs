using System;
using System.IO;
using StoryLine.Exceptions;

namespace StoryLine.Rest.Expectations
{
    public class StringContentComparer : IStringContentComparer
    {
        public void Verify(string expected, string actual)
        {
            if (actual == expected)
                return;

            using (var actualRdr = new StringReader(actual))
            using (var expectedRdr = new StringReader(expected))
            {
                string expectedLine;
                var lineNo = 1;

                while ((expectedLine = expectedRdr.ReadLine()) != null)
                {
                    var actualLine = actualRdr.ReadLine();

                    if (expectedLine.Equals(actualLine) == false)
                    {
                        var index = Zip(expectedLine, actualLine) + 1;

                        var message = $@"
Expected: 
 {expectedLine} 
Actual: 
 {actualLine} 
Differed on line {lineNo} and at character {index}";

                        throw new ExpectationException($"Expectation on body was not met {message}");
                    }
                    lineNo++;
                }
            }
        }

        private static int Zip(string expectedLine, string actualLine)
        {
            var length = Math.Min(expectedLine.Length, actualLine.Length);

            for (var i = 0; i < length; i++)
            {
                if (expectedLine[i] != actualLine[i])
                    return i;
            }

            return length;
        }
    }
}