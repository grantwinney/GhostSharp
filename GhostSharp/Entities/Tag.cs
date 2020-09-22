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

        /// <summary>
        /// Code Injected into Header
        /// </summary>
        [JsonProperty("codeinjection_head")]
        public string CodeInjectionHead { get; set; }

        /// <summary>
        /// Code Injected into Footer
        /// </summary>
        [JsonProperty("codeinjection_foot")]
        public string CodeInjectionFoot { get; set; }

        /// <summary>
        /// Facebook Card Image
        /// </summary>
        [JsonProperty("og_image")]
        public string OgImage { get; set; }

        /// <summary>
        /// Facebook Card Title
        /// </summary>
        [JsonProperty("og_title")]
        public string OgTitle { get; set; }

        /// <summary>
        /// Facebook Card Description
        /// </summary>
        [JsonProperty("og_description")]
        public string OgDescription { get; set; }

        /// <summary>
        /// Twitter Card Image
        /// </summary>
        [JsonProperty("twitter_image")]
        public string TwitterImage { get; set; }

        /// <summary>
        /// Twitter Card Title
        /// </summary>
        [JsonProperty("twitter_title")]
        public string TwitterTitle { get; set; }

        /// <summary>
        /// Twitter Card Description
        /// </summary>
        [JsonProperty("twitter_description")]
        public string TwitterDescription { get; set; }

        /// <summary>
        /// Canonical URL
        /// </summary>
        [JsonProperty("canonical_url")]
        public string CanonicalUrl { get; set; }

        /// <summary>
        /// Accent Color
        /// </summary>
        [JsonProperty("accent_color")]
        public string AccentColor { get; set; }
    }
}
