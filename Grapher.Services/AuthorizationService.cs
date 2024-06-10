using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Grapher.Base.DependencyInjection;
using Grapher.Domain.Models;
using Grapher.Services.Interfaces;

namespace Grapher.Services
{
	/// <inheritdoc cref="IAuthorizationService" />
	[Service(typeof(IAuthorizationService))]
	public class AuthorizationService : IAuthorizationService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		private readonly ILogger<AuthorizationService> _logger;
		private readonly ITaskService _taskService;
		private readonly HttpClient _requestClient;
		private readonly string _clientId;
		private readonly string _clientSecret;
		private readonly string _tenantId;
		private readonly string _appId;
		
		private string _tokenEndpoint = "https://login.microsoftonline.com/{0}/oauth2/v2.0/token";
		private string _bearerToken = string.Empty;
		private bool _isAuthorized = false;

		public AuthorizationService(IHttpClientFactory httpClientFactory, ILogger<AuthorizationService> logger, IConfiguration configuration, ITaskService taskService)
		{
			_logger = logger;
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
			_taskService = taskService;
			_requestClient = _httpClientFactory.CreateClient();

			_clientId = _configuration["Authorization:ClientId"] ?? string.Empty;
			_clientSecret = _configuration["Authorization:ClientSecret"] ?? string.Empty;
			_tenantId = _configuration["Authorization:TenantId"] ?? string.Empty;
			_appId = _configuration["Authorization:AppId"] ?? string.Empty;
			_tokenEndpoint = _tokenEndpoint.Replace("{0}", _tenantId);
		}

		public string GetApplicationId() => _appId ?? string.Empty;

		public string GetAccessToken()
		{
			if(!_isAuthorized)
			{
				_logger.LogInformation($"{nameof(GetAccessToken)} info: Grapher is unauthorized, getting a new bearer token.");
				return GetNewAccessToken();
			}

			_logger.LogInformation($"{nameof(GetAccessToken)} info: Grapher is authorized, getting the existing bearer token.");
			return _bearerToken;
		}

		/// <summary>
		/// Gets an access token for the purposes of sending authenticated requests to
		/// <see href="https://learn.microsoft.com/en-us/graph/">Microsoft's Graph API</see>.
		/// </summary>
		/// <returns>
		/// An access token to send in the request headers.
		/// </returns>
		private string GetNewAccessToken()
		{
			if (_isAuthorized)
			{
				_logger.LogInformation($"{nameof(GetAccessToken)} info: user already authenticated, no bearer token needed.");
				return string.Empty;
			}

			var authRequest = new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{ "grant_type", "client_credentials" },
				{ "client_id", _clientId },
				{ "client_secret", _clientSecret },
				{ "scope",  "https://graph.microsoft.com/.default" },
			});

			_logger.LogInformation($"{nameof(GetAccessToken)} info: attempting to post to {_tokenEndpoint} to recieve an access token.");
			var response = _taskService.GetTaskResult(_requestClient.PostAsync(_tokenEndpoint, authRequest));
			var authToken = _taskService.GetTaskResult(response.Content.ReadFromJsonAsync(typeof(AuthTokenResponse))) as AuthTokenResponse;

			if (string.IsNullOrEmpty(authToken?.AccessToken))
			{
				_logger.LogError($"{nameof(GetAccessToken)} error: the auth token endpoint returned an empty response, send back an empty string.");
				return string.Empty;
			}

			_logger.LogInformation($"{nameof(GetAccessToken)} info: bearer token successfully generated, setting authorized state for Grapher.");
			_logger.LogInformation($"{nameof(GetAccessToken)} response: {authToken}");
			_isAuthorized = true;
			return authToken.AccessToken;
		}
	}
}
