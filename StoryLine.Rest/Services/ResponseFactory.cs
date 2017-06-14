using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace StoryLine.Rest.Services
{
    public class ResponseFactory : IResponseFactory
    {
        public IResponse CreateExceptionResponse(IRequest request, Exception exception)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            return new Response
            {
                Request = request,
                Exception = exception
            };
        }

        public IResponse Create(IRequest request, HttpResponseMessage result)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            byte[] body;

            try
            {
                body = result.Content.ReadAsByteArrayAsync().Result;
            }
            catch (Exception ex)
            {
                return CreateExceptionResponse(request, ex);
            }

            var headers = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

            AddHeaders(headers, result.Content.Headers);
            AddHeaders(headers, result.Headers);

            return new Response
            {
                Request = request,
                Body = body,
                Status = (int)result.StatusCode,
                ReasonPhrase = result.ReasonPhrase,
                Headers = headers
            };
        }

        private static void AddHeaders(IDictionary<string, string[]> headers, IEnumerable<KeyValuePair<string, IEnumerable<string>>> resultHeaders)
        {
            foreach (var header in resultHeaders)
            {
                headers.Add(header.Key, header.Value.ToArray());
            }
        }
    }
}