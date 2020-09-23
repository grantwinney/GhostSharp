using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    public class ThemeEngines
    {
        /// <summary>
        /// Ghost Version
        /// </summary>
        [JsonProperty("ghost")]
        public string Ghost { get; set; }

        /// <summary>
        /// Ghost API Version
        /// </summary>
        [JsonProperty("ghost-api")]
        public string GhostAPI { get; set; }
    }
}
