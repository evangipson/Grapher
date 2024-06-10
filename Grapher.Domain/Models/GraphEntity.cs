using System.Text.Json.Serialization;

namespace Grapher.Domain.Models
{
	/// <summary>
	/// A domain representation of a collection of properties that are returned from the
	/// <see href="https://learn.microsoft.com/en-us/graph/">Microsoft Graph API</see>
	/// when requesting an 
	/// <see href="https://learn.microsoft.com/en-us/graph/api/resources/entity">entity</see>.
	/// </summary>
	public class GraphEntity : GraphResponse
	{
		/// <summary>
		/// The backing property for <see cref="BusinessPhones"/>.
		/// </summary>
		private IEnumerable<string>? _businessPhones;

		[JsonPropertyName("userPrincipalName")]
		public string? UserPrincipalName { get; set; }

		[JsonPropertyName("displayName")]
		public string? DisplayName { get; set; }

		[JsonPropertyName("surname")]
		public string? Surname { get; set; }

		[JsonPropertyName("givenName")]
		public string? GivenName { get; set; }

		[JsonPropertyName("preferredLanguage")]
		public string? PreferredLanguage { get; set; }

		[JsonPropertyName("mail")]
		public string? Mail { get; set; }

		[JsonPropertyName("mobilePhone")]
		public string? MobilePhone { get; set; }

		[JsonPropertyName("jobTitle")]
		public string? JobTitle { get; set; }

		[JsonPropertyName("officeLocation")]
		public string? OfficeLocation { get; set; }

		/// <summary>
		/// A collection of phone numbers for an entity's
		/// business. Will never be <c>null</c>, by default
		/// will be an empty list of <c>string</c>.
		/// </summary>
		[JsonPropertyName("businessPhones")]
		public IEnumerable<string> BusinessPhones
		{
			get => _businessPhones ?? (_businessPhones = new List<string>());
			set => _businessPhones = value;
		}
	}
}
