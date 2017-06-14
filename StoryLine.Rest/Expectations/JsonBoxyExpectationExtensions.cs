using System;

namespace StoryLine.Rest.Expectations
{
    public static class JsonBoxyExpectationExtensions
    {
        public static JsonBodyExpectationBuilder JsonBody(this HttpResponse builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return new JsonBodyExpectationBuilder(builder);
        }
    }
}