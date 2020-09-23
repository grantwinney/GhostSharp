using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    public class ThemeConfig
    {
        /// <summary>
        /// Posts Per Page
        /// </summary>
        [JsonProperty("posts_per_page")]
        public int PostsPerPage { get; set; }

        /// <summary>
        /// Image Sizes
        /// </summary>
        [JsonProperty("image_sizes")]
        public ThemeImageSizes ImageSizes { get; set; }
    }
}
