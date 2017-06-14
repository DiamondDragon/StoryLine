using System;
using Newtonsoft.Json;

namespace StoryLine.Wiremock.Builders
{
    public static class JsonBodyExtensions
    {
        public static ResponseBuilder JsonObjectBody(this ResponseBuilder builder, object content)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (content == null)
                return builder.Body("{}");

            return JsonObjectBody(builder, content, Config.DefaultJsonSerializerSettings);
        }

        public static ResponseBuilder JsonObjectBody(this ResponseBuilder builder, object content, JsonSerializerSettings settings)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (content == null)
                return builder.Body("{}");
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            return builder.Body(JsonConvert.SerializeObject(content, settings));
        }
    }
}