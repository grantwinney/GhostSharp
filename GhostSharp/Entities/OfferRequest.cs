using GhostSharp.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Request sent to Ghost
    /// </summary>
    public class OfferRequest
    {
        /// <summary>
        /// Represents your publication's offers (discounts or special prices) assigned to particular tiers.
        /// </summary>
        [Updateable]
        [JsonProperty("offers")]
        public List<Offer> Offers { get; set; }
    }
}
