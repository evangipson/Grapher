using Grapher.Base.DependencyInjection;
using Grapher.Domain.Models;
using Grapher.Services.Interfaces;

namespace Grapher.Services
{
    /// <inheritdoc cref="IRequestService" />
    [Service(typeof(IRequestService))]
    public class RequestService : IRequestService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _requestClient;

        public RequestService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _requestClient = _httpClientFactory.CreateClient();
        }

        public GraphEntity GetGraphEntity()
        {
            return new GraphEntity
            {
                Id = "GraphResponse_Id",
                ODataContext = "GraphResponse_ODataContext"
            };
        }
    }
}
