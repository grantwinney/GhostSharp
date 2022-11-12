using GhostSharp.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Request sent to Ghost
    /// </summary>
    public class TierRequest
    {
        /// <summary>
        /// Collection of tiers.
        /// </summary>
        [JsonProperty("tiers")]
        [RequiredForUpdate]
        public List<Tier> Tiers { get; set; }
    }
}
