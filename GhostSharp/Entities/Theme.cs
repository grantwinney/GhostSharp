using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Theme
    /// </summary>
    public class Theme
    {
        /// <summary>
        /// Name of the theme
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Meta data contained in the theme package
        /// </summary>
        [JsonProperty("package")]
        public Package Package { get; set; }

        /// <summary>
        /// Flag indicating whether or not the theme is active
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Templates
        /// </summary>
        [JsonProperty("templates")]
        public List<string> Templates { get; set; }
    }
}
