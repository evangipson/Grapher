using Grapher.Domain.Models;

namespace Grapher.Services.Interfaces
{
    /// <summary>
    /// A service that will utilize an <see cref="HttpClient"/>
    /// to communicate with <see href="https://learn.microsoft.com/en-us/graph/">Microsoft's Graph API</see>.
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// Attempts to GET an <see href="https://learn.microsoft.com/en-us/graph/api/resources/entity">entity</see>
        /// from <see href="https://learn.microsoft.com/en-us/graph/">Microsoft's Graph API</see>.
        /// </summary>
        /// <returns>
        /// An <see href="https://learn.microsoft.com/en-us/graph/api/resources/entity">entity</see>
        /// from <see href="https://learn.microsoft.com/en-us/graph/">Microsoft's Graph API</see>,
        /// as a domain <see cref="GraphEntity"/>.
        /// </returns>
        GraphEntity GetGraphEntity();
    }
}
