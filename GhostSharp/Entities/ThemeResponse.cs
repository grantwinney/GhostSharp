using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing a theme
    /// </summary>
    public class ThemeResponse
    {
        /// <summary>
        /// Theme
        /// </summary>
        [JsonProperty("themes")]
        public List<Theme> Themes { get; set; }
    }
}
