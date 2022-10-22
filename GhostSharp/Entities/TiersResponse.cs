using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing tiers and any meta data
    /// </summary>
    public class TiersResponse
    {
        /// <summary>
        /// Represents your publication's tiers.
        /// </summary>
        [JsonProperty("tiers")]
        public Tiers Tiers { get; set; }

        /// <summary>
        /// Meta data regarding the response.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
