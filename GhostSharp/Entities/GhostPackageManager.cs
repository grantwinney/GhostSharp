using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    public class GhostPackageManager
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Categories
        /// </summary>
        [JsonProperty("categories")]
        public List<string> Categories { get; set; }
    }
}
