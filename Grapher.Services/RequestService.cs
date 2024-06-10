using System.Net.Http.Headers;
using System.Net.Http.Json;

using Grapher.Base.DependencyInjection;
using Grapher.Domain.Models;
using Grapher.Services.Interfaces;

namespace Grapher.Services
{
	/// <inheritdoc cref="IRequestService" />
	[Service(typeof(IRequestService))]
	public class RequestService : IRequestService
	{
		private readonly ITaskService _taskService;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IAuthorizationService _authorizationService;
		private readonly HttpClient _requestClient;
		private static readonly Uri _baseEndpoint = new Uri("https://graph.microsoft.com");

		public RequestService(ITaskService taskService, IHttpClientFactory httpClientFactory, IAuthorizationService authorizationService)
		{
			_taskService = taskService;
			_httpClientFactory = httpClientFactory;
			_authorizationService = authorizationService;
			_requestClient = _httpClientFactory.CreateClient();
		}

		public GraphEntity GetGraphEntity()
		{
			var entityRequestUri = GetEndpoint("me");
			var response = SendGraphQuery(entityRequestUri);
			var entityResponse = _taskService.GetTaskResult(response.Content.ReadFromJsonAsync(typeof(GraphEntity))) as GraphEntity;
			return entityResponse ?? new GraphEntity();
		}

		public GraphApplication GetGraphApplication()
		{
			var applicationPath = string.Join('/', "applications", _authorizationService.GetApplicationId());
			var applicationRequestUri = GetEndpoint(applicationPath);
			var response = SendGraphQuery(applicationRequestUri);
			var applicationResponse = _taskService.GetTaskResult(response.Content.ReadFromJsonAsync(typeof(GraphApplication))) as GraphApplication;
			return applicationResponse ?? new GraphApplication();
		}

		private Uri GetEndpoint(string endpointPath) => new Uri(_baseEndpoint, string.Join('/', "v1.0", endpointPath));

		private HttpResponseMessage SendGraphQuery(Uri endpoint)
		{
			var requestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);
			requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authorizationService.GetAccessToken());
			return _taskService.GetTaskResult(_requestClient.SendAsync(requestMessage));
		}
	}
}
