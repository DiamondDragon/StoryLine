using System;

namespace StoryLine.Rest.Expectations
{
    public static class TextBodyExpectationExtensions
    {
        public static TextBodyExpectationBuilder TextBody(this HttpResponse builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return new TextBodyExpectationBuilder(builder);
        }
    }
}