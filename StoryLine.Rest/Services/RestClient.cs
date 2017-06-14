using System;

namespace StoryLine.Rest.Services
{
    public class RestClient : IRestClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IRequestMessageFactory _requestMessageFactory;
        private readonly IResponseFactory _responseFactory;

        public RestClient(IHttpClientFactory clientFactory, IRequestMessageFactory requestMessageFactory, IResponseFactory responseFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _requestMessageFactory = requestMessageFactory ?? throw new ArgumentNullException(nameof(requestMessageFactory));
            _responseFactory = responseFactory ?? throw new ArgumentNullException(nameof(responseFactory));
        }

        public IResponse Send(IRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (var client = _clientFactory.Create(request.Service))
            {
                var requestMessage = _requestMessageFactory.Create(request);

                try
                {
                    return _responseFactory.Create(
                        request,
                        client.SendAsync(requestMessage).Result);
                }
                catch (Exception ex)
                {
                    return _responseFactory.CreateExceptionResponse(request, ex);
                }
            }
        }
    }
}
