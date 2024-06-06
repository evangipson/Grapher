using System.Text.Json.Serialization;

namespace Grapher.Base.Models
{
	/// <summary>
	/// The lowest-level of common information returned from
	/// <see href="https://learn.microsoft.com/en-us/graph/">Microsoft's Graph API</see>.
	/// Intended to be inherited by more complex response types.
	/// </summary>
	public abstract class GraphResponse
	{
		/// <summary>
		/// The id that is returned from a
		/// <see href="https://learn.microsoft.com/en-us/graph/">Microsoft Graph API</see>
		/// request. Will not be <c>null</c>, and is <see cref="string.Empty"/> by default.
		/// </summary>
		[JsonPropertyName("id")]
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// The <c><see href="https://learn.microsoft.com/en-us/graph/traverse-the-graph?tabs=http#microsoft-graph-api-metadata">@odata.context</see></c>
		/// property that is returned from a <see href="https://learn.microsoft.com/en-us/graph/">Microsoft Graph API</see>
		/// request. Will not be <c>null</c>, and is <see cref="string.Empty"/> by default.
		/// </summary>
		[JsonPropertyName("@odata.context")]
		public string ODataContext { get; set; } = string.Empty;
	}
}
