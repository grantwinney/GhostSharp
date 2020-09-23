using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    public class ThemeScripts
    {
        /// <summary>
        /// Dev
        /// </summary>
        [JsonProperty("dev")]
        public string Dev { get; set; }

        /// <summary>
        /// Zip
        /// </summary>
        [JsonProperty("zip")]
        public string Zip { get; set; }

        /// <summary>
        /// Test
        /// </summary>
        [JsonProperty("test")]
        public string Test { get; set; }

        /// <summary>
        /// Test CI
        /// </summary>
        [JsonProperty("test:ci")]
        public string TestCI { get; set; }

        /// <summary>
        /// Pre-Test
        /// </summary>
        [JsonProperty("pretest")]
        public string PreTest { get; set; }
        /// <summary>
        /// Pre-Ship
        /// </summary>
        [JsonProperty("preship")]
        public string PreShip { get; set; }

        /// <summary>
        /// Ship
        /// </summary>
        [JsonProperty("ship")]
        public string Ship { get; set; }

        /// <summary>
        /// Post-Ship
        /// </summary>
        [JsonProperty("postship")]
        public string PostShip { get; set; }
    }
}
