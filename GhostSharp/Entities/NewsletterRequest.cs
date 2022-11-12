using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Request sent to Ghost
    /// </summary>
    public class NewsletterRequest
    {
        /// <summary>
        /// Represents your publication's newsletters
        /// </summary>
        [JsonProperty("newsletters")]
        public List<Newsletter> Newsletters { get; set; }
    }
}
