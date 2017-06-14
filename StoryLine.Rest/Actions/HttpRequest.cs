using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using StoryLine.Contracts;

namespace StoryLine.Rest.Actions
{
    public class HttpRequest : IActionBuilder
    {
        private static readonly string DefaultUrl = string.Empty;

        private readonly Dictionary<string, List<string>> _headers = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, List<string>> _queryParameters = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        private string _method = "GET";
        private string _path;
        private string _url;
        private string _service;
        private byte[] _body;

        public HttpRequest Service(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

            _service = value;

            return this;
        }

        public HttpRequest Method(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

            _method = value;

            return this;
        }

        public HttpRequest Url(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

            if (!string.IsNullOrEmpty(_path))
                throw new InvalidOperationException($"Method {nameof(Url)}() can't be used if {nameof(Path)}() was already executed.");

            _url = value;

            return this;
        }

        public HttpRequest Path(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

            if (!string.IsNullOrEmpty(_path))
                throw new InvalidOperationException($"Method {nameof(Path)}() can't be used if {nameof(Url)}() was already executed.");

            _path = value;

            return this;
        }

        public HttpRequest QueryParameter(string parameter, string value)
        {
            if (string.IsNullOrWhiteSpace(parameter))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(parameter));
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

            AddDictionaryValue(_queryParameters, parameter, value);

            return this;
        }

        public HttpRequest Header(string header, string value)
        {
            if (string.IsNullOrWhiteSpace(header))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(header));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            AddDictionaryValue(_headers, header, value);

            return this;
        }

        public HttpRequest Body(byte[] value)
        {
            _body = value ?? throw new ArgumentNullException(nameof(value));

            return this;
        }

        IAction IActionBuilder.Build()
        {
            return new HttpRequestAction
            {
                Service = _service,
                Body = _body,
                Headers = _headers.ToDictionary(x => x.Key, x => x.Value.ToArray(), StringComparer.OrdinalIgnoreCase),
                Method = _method,
                Url = BuildUrl()
            };
        }

        private string BuildUrl()
        {
            if (string.IsNullOrEmpty(_path))
                return _url ?? DefaultUrl;

            var builder = new StringBuilder();

            foreach (var header in _queryParameters)
            {
                foreach (var value in header.Value)
                {
                    if (builder.Length > 0)
                        builder.Append('&');

                    builder.Append(WebUtility.UrlEncode(header.Key));
                    builder.Append('=');
                    builder.Append(WebUtility.UrlEncode(value));
                }
            }

            if (builder.Length > 0)
                builder.Insert(0, '?');

            builder.Insert(0, _path ?? DefaultUrl);

            return builder.ToString();
        }

        private static void AddDictionaryValue(Dictionary<string, List<string>> headers, string header, string value)
        {
            if (!headers.ContainsKey(header))
                headers.Add(header, new List<string>());

            headers[header].Add(value);
        }
    }
}