using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    public class Package
    {
        /// <summary>
        /// Theme Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Theme Description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Demo URL
        /// </summary>
        [JsonProperty("demo")]
        public string Demo { get; set; }

        /// <summary>
        /// Version Number
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Engines
        /// </summary>
        [JsonProperty("engines")]
        public ThemeEngines Engines { get; set; }

        /// <summary>
        /// License Name
        /// </summary>
        [JsonProperty("license")]
        public string License { get; set; }

        /// <summary>
        /// Screenshots
        /// </summary>
        [JsonProperty("screenshots")]
        public ThemeScreenshots ScreenShots { get; set; }

        /// <summary>
        /// Scripts
        /// </summary>
        [JsonProperty("scripts")]
        public ThemeScripts Scripts { get; set; }

        /// <summary>
        /// Author
        /// </summary>
        [JsonProperty("author")]
        public ThemeAuthor Author { get; set; }

        /// <summary>
        /// Ghost Package Manager Info
        /// </summary>
        [JsonProperty("gpm")]
        public GhostPackageManager GPM { get; set; }

        /// <summary>
        /// Keywords
        /// </summary>
        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; }

        /// <summary>
        /// Repository Info
        /// </summary>
        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        /// <summary>
        /// Bugs URL
        /// </summary>
        [JsonProperty("bugs")]
        public string Bugs { get; set; }

        /// <summary>
        /// Contributors URL
        /// </summary>
        [JsonProperty("contributors")]
        public string Contributors { get; set; }

        /// <summary>
        /// Dev Dependencies
        /// </summary>
        [JsonProperty("devDependencies")]
        public Dictionary<string, string> DevDependencies { get; set; }

        /// <summary>
        /// Browsers
        /// </summary>
        [JsonProperty("browserslist")]
        public List<string> BrowsersList { get; set; }

        /// <summary>
        /// Configuration Info
        /// </summary>
        [JsonProperty("config")]
        public ThemeConfig Config { get; set; }
    }
}
