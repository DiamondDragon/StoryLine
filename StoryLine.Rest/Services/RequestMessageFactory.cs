using System;
using System.Collections.Generic;
using System.Net.Http;

namespace StoryLine.Rest.Services
{
    public class RequestMessageFactory : IRequestMessageFactory
    {
        private static readonly HashSet<string> BodyLessMethdods = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "GET",
            "HEAD"
        };

        private static readonly HashSet<string> ContentHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Allow",
            "Content-Disposition",
            "Content-Encoding",
            "Content-Language",
            "Content-Location",
            "Content-MD5",
            "Content-Range",
            "Content-Type",
            "Expires",
            "Last-Modified",
        };

        private static readonly HashSet<string> IgnoredHaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Content-Length"
        };

        public HttpRequestMessage Create(IRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var message = new HttpRequestMessage
            {
                Method = new HttpMethod(request.Method.ToUpper()),
                RequestUri = new Uri(request.Url, UriKind.RelativeOrAbsolute),
                Content = CreateContent(request),
            };

            foreach (var header in request.Headers)
            {
                if (IgnoredHaders.Contains(header.Key))
                    continue;

                if (!ContentHeaders.Contains(header.Key))
                    message.Headers.Add(header.Key, header.Value);
            }

            return message;
        }

        private static HttpContent CreateContent(IRequest request)
        {
            if (BodyLessMethdods.Contains(request.Method))
                return null;

            if (request.Body == null)
                return null;

            if (request.Body.Length == 0)
                return null;

            var content = new ByteArrayContent(request.Body);

            foreach (var header in request.Headers)
            {
                if (IgnoredHaders.Contains(header.Key))
                    continue;

                if (ContentHeaders.Contains(header.Key))
                    content.Headers.Add(header.Key, header.Value);
            }

            return content;
        }
    }
}