using GhostSharp.Entities;
using GhostSharp.QueryParams;

namespace GhostSharp
{
    public partial class GhostContentAPI
    {
        /// <summary>
        /// Get the settings for the blog, including title, description,
        /// code injected into the header or footer (if any), etc.
        /// </summary>
        /// <returns>The blog settings.</returns>
        public new TiersResponse GetTiers(TierQueryParams queryParams = null)
        {
            return base.GetTiers(queryParams);
        }
    }
}
