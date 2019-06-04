using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing settings and any meta data
    /// </summary>
    public class SettingsResponse
    {
        /// <summary>
        /// Represents your publication's settings.
        /// </summary>
        [JsonProperty("settings")]
        public Settings Settings { get; set; }

        /// <summary>
        /// Meta data regarding the response.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
