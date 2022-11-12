using Newtonsoft.Json;
using System;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents a webhook.
    /// </summary>
    /// <seealso cref="https://ghost.org/docs/admin-api/#webhooks"/>
    public class Webhook
    {
        /// <summary>
        /// The unique ID of the webook.
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; set; }

        /// <summary>
        /// The event to which the webhook applies.
        /// (See https://ghost.org/docs/webhooks/#available-events)
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; }

        /// <summary>
        /// The target URL.
        /// </summary>
        [JsonProperty("target_url")]
        public string TargetURL { get; set; }

        /// <summary>
        /// The name for the webhook.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The secret.
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; set; }

        /// <summary>
        /// The API version to which this webhook applies.
        /// </summary>
        [JsonProperty("api_version")]
        public string APIVersion { get; set; }

        /// <summary>
        /// The integration to which this webhook applies.
        /// </summary>
        [JsonProperty("integration_id")]
        public string IntegrationID { get; set; }

        /// <summary>
        /// The status of the webhook. (i.e. available)
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Last triggered at
        /// </summary>
        [JsonProperty("last_triggered_at")]
        public DateTime? LastTriggeredAt { get; private set; }

        /// <summary>
        /// Last triggered status
        /// </summary>
        [JsonProperty("last_triggered_status")]
        public string LastTriggeredStatus { get; private set; }

        /// <summary>
        /// Last triggered error
        /// </summary>
        [JsonProperty("last_triggered_error")]
        public string LastTriggeredError { get; private set; }

        /// <summary>
        /// Created At
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// Updated At
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; private set; }
    }
}
