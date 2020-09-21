using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents a Tag.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Slug
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Feature Image
        /// </summary>
        [JsonProperty("feature_image")]
        public string FeatureImage { get; set; }

        /// <summary>
        /// Visibility
        /// </summary>
        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        /// <summary>
        /// Meta Title
        /// </summary>
        [JsonProperty("meta_title")]
        public string MetaTitle { get; set; }

        /// <summary>
        /// Meta Description
        /// </summary>
        [JsonProperty("meta_description")]
        public string MetaDescription { get; set; }

        /// <summary>
        /// Post Count
        /// </summary>
        [JsonProperty("count")]
        public Count Count { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        //"og_image": null,
        //"og_title": null,
        //"og_description": null,
        //"twitter_image": null,
        //"twitter_title": null,
        //"twitter_description": null,
        //"codeinjection_head": null,
        //"codeinjection_foot": null,
        //"canonical_url": null,
        //"accent_color": null,
    }
}
