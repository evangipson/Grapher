using System.Text.Json.Serialization;

namespace Grapher.Base.Models
{
	public class AuthTokenResponse
	{
		[JsonPropertyName("access_token")]
		public string? AccessToken { get; set; }
	}
}
