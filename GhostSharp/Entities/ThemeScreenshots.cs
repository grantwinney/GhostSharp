using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    public class ThemeScreenshots
    {
        /// <summary>
        /// Desktop Screenshot
        /// </summary>
        [JsonProperty("desktop")]
        public string Desktop { get; set; }

        /// <summary>
        /// Mobile Screenshot
        /// </summary>
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
    }
}
