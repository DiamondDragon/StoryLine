using System;
using System.Text;
using Newtonsoft.Json;

namespace StoryLine.Rest.Actions
{
    public class JsonBodyBuilder
    {
        private readonly HttpRequest _builder;

        public JsonBodyBuilder(HttpRequest request)
        {
            _builder = request ?? throw new ArgumentNullException(nameof(request));
        }

        public HttpRequest FromResourceFile()
        {
            return FromResourceFile(Config.DefaultEncoding);
        }

        public HttpRequest FromResourceFile(Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));


            throw new NotImplementedException();
        }

        public HttpRequest FromObject(object value)
        {
            return FromObject(value, Config.DefaultJsonSerializerSettings);
        }

        public HttpRequest FromObject(object value, JsonSerializerSettings settings)
        {
            return FromObject(value, settings, Config.DefaultEncoding);
        }

        public HttpRequest FromObject(object value, JsonSerializerSettings settings, Encoding encoding)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            _builder.Body(encoding.GetBytes(JsonConvert.SerializeObject(value, settings)));

            return _builder;
        }
    }
}