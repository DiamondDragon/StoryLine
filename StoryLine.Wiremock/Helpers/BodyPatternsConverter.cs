using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StoryLine.Wiremock.Helpers
{
    public class BodyPatternsConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = (List<KeyValuePair<string, object>>)value;

            writer.WriteStartArray();
            foreach (var item in list)
            {
                writer.WriteRawValue(GetPredicateObjectJson(item));
            }
            writer.WriteEndArray();
        }

        private static string GetPredicateObjectJson(KeyValuePair<string, object> item)
        {
            var predicateObject = item.Value;

            if (!(item.Value is string))
                return JsonConvert.SerializeObject(predicateObject);

            predicateObject = new JObject
            {
                { item.Key, (string)item.Value }
            };
            return predicateObject.ToString();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<KeyValuePair<string, string>>);
        }
    }
}