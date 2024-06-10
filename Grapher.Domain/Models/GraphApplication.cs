using System.Text.Json.Serialization;

namespace Grapher.Domain.Models
{
	/// <summary>
	/// A domain representation of a collection of properties that are returned from the
	/// <see href="https://learn.microsoft.com/en-us/graph/">Microsoft Graph API</see>
	/// when requesting an 
	/// <see href="https://learn.microsoft.com/en-us/graph/api/resources/application">application</see>.
	/// </summary>
	public class GraphApplication : GraphResponse
	{
		/// <summary>
		/// The backing property for <see cref="IdentifierUris"/>.
		/// </summary>
		private IEnumerable<string>? _identifierUris;

		[JsonPropertyName("createdDateTIme")]
		public string? CreatedDateTime { get; set; }

		[JsonPropertyName("appId")]
		public string? AppId{ get; set; }

		[JsonPropertyName("displayName")]
		public string? DisplayName { get; set; }

		/// <summary>
		/// A collection of identifier uris for an application.
		/// Will never be <c>null</c>, by default will be an
		/// empty list of <c>string</c>.
		/// </summary>
		[JsonPropertyName("identifierUris")]
		public IEnumerable<string> IdentifierUris
		{
			get => _identifierUris ?? (_identifierUris = new List<string>());
			set => _identifierUris = value;
		}
	}
}
