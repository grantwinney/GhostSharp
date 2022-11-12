using GhostSharp.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents a tier.
    /// </summary>
    public class Tier
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
        /// The slug for the tier. (i.e. free, gold)
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// A flag representing whether or not the tier is active.
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// The type of tier. (i.e. free, paid)
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
        /// Monthly price. (note: 1000 = $10.00)
        /// </summary>
        [JsonProperty("monthly_price")]
        public int? MonthlyPrice { get; set; }

        /// <summary>
        /// Yearly price. (note: 12000 = $120.00)
        /// </summary>
        [JsonProperty("yearly_price")]
        public int? YearlyPrice { get; set; }

        /// <summary>
        /// Currency of prices.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Benefits of the tier.
        /// </summary>
        [JsonProperty("benefits")]
        public List<string> Benefits { get; set; }

        /// <summary>
        /// Trial days.
        /// </summary>
        [JsonProperty("trial_days")]
        public int TrialDays { get; set; }

        /// <summary>
        /// Visibility of the tier. (i.e. public, none)
        /// </summary>
        [JsonProperty("visibility")]
        public string Visibility { get; set; }
    }
}
