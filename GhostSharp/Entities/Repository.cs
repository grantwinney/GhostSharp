using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    public class Repository
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        [JsonProperty("url")]
        public string URL { get; set; }
    }
}
