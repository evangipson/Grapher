using Grapher.Base.Models;

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

        public string? UserPrincipalName { get; set; }

        public string? DisplayName { get; set; }

        public string? Surname { get; set; }

        public string? GivenName { get; set; }

        public string? PreferredLanguage { get; set; }

        public string? Mail { get; set; }

        public string? MobilePhone { get; set; }

        public string? JobTitle { get; set; }

        public string? OfficeLocation { get; set; }

        /// <summary>
        /// A collection of phone numbers for an entity's
        /// business. Will never be <c>null</c>, by default
        /// will be an empty list of <c>string</c>.
        /// </summary>
        public IEnumerable<string> BusinessPhones
        {
            get => _businessPhones ?? (_businessPhones = new List<string>());
            set => _businessPhones = value;
        }
    }
}
