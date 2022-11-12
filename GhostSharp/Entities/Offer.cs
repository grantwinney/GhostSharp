using GhostSharp.Attributes;
using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents an offer.
    /// </summary>
    public class Offer
    {
        /// <summary>
        /// The unique ID of the offer
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; set; }

        /// <summary>
        /// Internal name for an offer, must be unique (max length 40 chars)
        /// </summary>
        [Updateable]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Shortcode for the offer, for example: https://yoursite.com/black-friday
        /// </summary>
        [Updateable]
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Name displayed in the offer window
        /// </summary>
        [Updateable]
        [JsonProperty("display_title")]
        public string DisplayTitle { get; set; }

        /// <summary>
        /// Text displayed in the offer window
        /// </summary>
        [Updateable]
        [JsonProperty("display_description")]
        public string DisplayDescription { get; set; }

        /// <summary>
        /// Whether the amount off is a percentage or fixed (i.e. percent, fixed)
        /// </summary>
        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        /// <summary>
        /// Denotes if offer applies to tier's monthly or yearly price. (i.e. month, year)
        /// </summary>
        [JsonProperty("cadence", Required = Required.Always)]
        public string Cadence { get; set; }

        /// <summary>
        /// Offer discount amount, as a percentage or fixed value as set in type. Amount is always denoted by the smallest currency unit (e.g., 100 cents instead of $1.00 in USD)
        /// </summary>
        [JsonProperty("amount", Required = Required.Always)]
        public int Amount { get; set; }

        /// <summary>
        /// The duration of the offer. (i.e. once)
        /// </summary>
        [JsonProperty("duration", Required = Required.Always)]
        public string Duration { get; set; }

        /// <summary>
        /// Number of months offer should be repeated when duration is repeating
        /// </summary>
        [JsonProperty("duration_in_months")]
        public string DurationInMonths { get; set; }

        /// <summary>
        /// Denotes whether the offer `currency` is restricted. If so, changing the currency invalidates the offer
        /// </summary>
        [JsonProperty("currency_restriction")]
        public bool CurrencyRestriction { get; set; }

        /// <summary>
        /// Specifies offer's currency as three letter ISO currency code; applies to 'fixed' type offers only and must match the tier's currency (i.e. usd)
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Denotes if the offer is active or archived (i.e. active, archived)
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Number of times the offer has been redeemed
        /// </summary>
        [JsonProperty("redemption_count")]
        public int RedemptionCount { get; private set; }

        /// <summary>
        /// Tier on which offer is applied
        /// </summary>
        [JsonProperty("tier")]
        public Tier Tier { get; set; }
    }
}
