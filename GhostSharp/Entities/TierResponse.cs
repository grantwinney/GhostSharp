using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing tiers and any meta data
    /// </summary>
    public class TierResponse
    {
        /// <summary>
        /// Represents your publication's tiers.
        /// </summary>
        [JsonProperty("tiers")]
        public List<Tier> Tiers { get; set; }

        /// <summary>
        /// Meta data regarding the response.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
