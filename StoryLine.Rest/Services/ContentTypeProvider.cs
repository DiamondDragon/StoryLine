using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryLine.Rest.Services
{
    public class ContentTypeProvider : IContentTypeProvider
    {
        private static readonly string[] EmptyArray = new string[0];

        public string GetContentType(IEnumerable<KeyValuePair<string, string[]>> headers)
        {
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            var values = GetContentTypeValues(headers);

            return values.FirstOrDefault() ?? string.Empty;
        }

        public string GetCharSet(IEnumerable<KeyValuePair<string, string[]>> headers)
        {
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            var values = GetContentTypeValues(headers);

            if (values.Length != 2)
                return string.Empty;

            var encoding = values[1].Split('=');
            if (encoding.Length != 2)
                return string.Empty;

            return encoding[1].Trim();
        }

        public string GetContentType(string contentTypeMediaType)
        {
            if (string.IsNullOrEmpty(contentTypeMediaType))
                throw new ArgumentNullException(nameof(contentTypeMediaType));

            var values = contentTypeMediaType.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            return 
                (from value in values
                 select value.Trim())
                .FirstOrDefault() ?? string.Empty;
        }

        private static string[] GetContentTypeValues(IEnumerable<KeyValuePair<string, string[]>> headers)
        {
            var contentType = headers.FirstOrDefault(x => x.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(contentType.Key))
                return EmptyArray;

            var contentTypeValue = contentType.Value.FirstOrDefault();
            if (string.IsNullOrEmpty(contentTypeValue))
                return EmptyArray;

            var values = contentTypeValue.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            return
                (from value in values
                 select value.Trim())
                .ToArray();
        }
    }
}