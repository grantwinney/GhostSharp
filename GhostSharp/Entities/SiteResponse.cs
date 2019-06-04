using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing basic information about a site.
    /// </summary>
    public class SiteResponse
    {
        /// <summary>
        /// Basic information about a site
        /// </summary>
        [JsonProperty("site")]
        public Site Site { get; set; }
    }
}
