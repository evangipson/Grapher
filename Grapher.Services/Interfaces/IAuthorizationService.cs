using System.Net.Http.Headers;

namespace Grapher.Services.Interfaces
{
	/// <summary>
	/// Provides required authorization information for sending requests to
	/// <see href="https://learn.microsoft.com/en-us/graph/">Microsoft's Graph API</see>.
	/// </summary>
	public interface IAuthorizationService
	{
		/// <summary>
		/// Gets the application id for Grapher.
		/// </summary>
		/// <returns>
		/// The application id for Grapher, or
		/// <see cref="string.Empty"> if it has
		/// not been set.
		/// </returns>
		string GetApplicationId();

		/// <summary>
		/// Gets the access token necessary to make a request to
		/// <see href="https://learn.microsoft.com/en-us/graph/">Microsoft's Graph API</see>.
		/// </summary>
		/// <returns>
		/// The access token needed to send a request.
		/// </returns>
		string GetAccessToken();
	}
}
