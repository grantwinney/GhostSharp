using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Basic information about a site
    /// </summary>
    /// <remarks>
    /// Site is a special unauthenticated, read-only endpoint for retrieving basic information about a site.
    /// This information is useful for integrations and clients that need to show some details of a site before providing authentication.
    /// </remarks>
    public class Site
    {
        /// <summary>
        /// Title
        /// </summary>
        /// <remarks>
        /// Same as the title returned from the Content API settings endpoint.
        /// </remarks>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// <remarks>
        /// Same as the description returned from the Content API settings endpoint.
        /// </remarks>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        /// <remarks>
        /// Same as the logo returned from the Content API settings endpoint.
        /// </remarks>
        [JsonProperty("logo")]
        public string Logo { get; set; }

        /// <summary>
        /// The frontend URL for the site, which can be different to the Admin / API URL.
        /// </summary>
        /// <remarks>
        /// This comes from the configuration JSON file.
        /// </remarks>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// The current version of the Ghost site. (Semver String - major.minor)
        /// </summary>
        /// <remarks>
        /// Use this to check the minimum version is high enough for compatibility with integrations.
        /// </remarks>
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
