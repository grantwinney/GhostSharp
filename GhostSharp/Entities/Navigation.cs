using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents a single menu item on your publication.
    /// </summary>
    public class Navigation
    {
        /// <summary>
        /// Menu label as visitors see it
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Menu URL (relative URL, such as /about-me)
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
