using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing offers
    /// </summary>
    public class OfferResponse
    {
        /// <summary>
        /// Represents your publication's offers (discounts or special prices) assigned to particular tiers.
        /// </summary>
        [JsonProperty("offers")]
        public List<Offer> Offers { get; set; }
    }
}
