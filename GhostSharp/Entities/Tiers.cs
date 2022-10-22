using GhostSharp.Attributes;
using Newtonsoft.Json;
using System;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents your publication's tiers.
    /// </summary>
    public class Tiers
    {
        /// <summary>
        /// The unique ID of the tier.
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; set; }

        /// <summary>
        /// The name of the tier.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The description of the tier.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The slug for the tier. (i.e. free or gold)
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// A flag representing whether or not the tier is active.
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// The type of tier. (i.e. free or paid)
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The welcome page url.
        /// </summary>
        [JsonProperty("welcome_page_url")]
        public string WelcomePageURL { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// Updated At
        /// </summary>
        [JsonProperty("updated_at")]
        [RequiredForUpdate]  // TODO: Needed?
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// Stripe prices for the tier.
        /// </summary>
        [JsonProperty("stripe_prices")]
        public string StripePrices { get; set; }

        /// <summary>
        /// Benefits of the tier.
        /// </summary>
        [JsonProperty("benefits")]
        public string Benefits { get; set; }

        /// <summary>
        /// Visibility of the tier. (i.e. public)
        /// </summary>
        [JsonProperty("visibility")]
        public string Visibility { get; set; }

    }
}
