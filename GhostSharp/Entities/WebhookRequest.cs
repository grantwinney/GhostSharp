using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Request sent to Ghost
    /// </summary>
    public class WebhookRequest
    {
        /// <summary>
        /// Represents your site's webhooks
        /// </summary>
        [JsonProperty("webhooks")]
        public List<Webhook> Webhooks { get; set; }
    }
}
