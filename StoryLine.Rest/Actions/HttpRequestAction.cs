using System;
using System.Collections.Generic;
using StoryLine.Contracts;
using StoryLine.Rest.Services;

namespace StoryLine.Rest.Actions
{
    public class HttpRequestAction : IAction
    {
        private string _method = "GET";
        private string _url = string.Empty;
        private IReadOnlyDictionary<string, string[]> _headers = new Dictionary<string, string[]>();

        public string Service { get; set; }

        public string Method
        {
            get => _method;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(Method) + " can't be null or whitespace string");

                _method = value;
            }
        }

        public string Url
        {
            get => _url;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(Url) + " can't be null or whitespace string");

                _url = value;
            }
        }

        public IReadOnlyDictionary<string, string[]> Headers
        {
            get => _headers;
            set => _headers = value ?? throw new ArgumentNullException(nameof(value));
        }

        public byte[] Body { get; set; }

        public void Execute(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            var request = new Request
            {
                Service = Service,
                Headers = Headers,
                Body = Body,
                Method = Method,
                Url = Url
            };

            actor.Artifacts.Add(Config.RestClient.Send(request));
        }
    }
}