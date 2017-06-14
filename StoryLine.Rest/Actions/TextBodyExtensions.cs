using System;
using System.Text;

namespace StoryLine.Rest.Actions
{
    public static class TextBodyExtensions
    {
        public static HttpRequest TextBody(this HttpRequest builder, string value)
        {
            return TextBody(builder, value, Config.DefaultEncoding);
        }

        public static HttpRequest TextBody(this HttpRequest builder, string value, Encoding encoding)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            builder.Body(encoding.GetBytes(value));

            return builder;
        }
    }
}