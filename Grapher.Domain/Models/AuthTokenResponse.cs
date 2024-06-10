using System.Text.Json.Serialization;

namespace Grapher.Domain.Models
{
	public class AuthTokenResponse
	{
		[JsonPropertyName("access_token")]
		public string? AccessToken { get; set; }
	}
}
