using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    public class ThemeImageSizes
    {
        /// <summary>
        /// Extra Extra Small
        /// </summary>
        [JsonProperty("xxs")]
        public ThemeImageSizeWidth XXS { get; set; }

        /// <summary>
        /// Extra Small
        /// </summary>
        [JsonProperty("xs")]
        public ThemeImageSizeWidth XS { get; set; }

        /// <summary>
        /// Small
        /// </summary>
        [JsonProperty("s")]
        public ThemeImageSizeWidth S { get; set; }

        /// <summary>
        /// Medium
        /// </summary>
        [JsonProperty("m")]
        public ThemeImageSizeWidth M { get; set; }

        /// <summary>
        /// Large
        /// </summary>
        [JsonProperty("l")]
        public ThemeImageSizeWidth L { get; set; }

        /// <summary>
        /// Extra Large
        /// </summary>
        [JsonProperty("xl")]
        public ThemeImageSizeWidth XL { get; set; }
    }
}
