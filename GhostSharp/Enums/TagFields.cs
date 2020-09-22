using Newtonsoft.Json;
using System;

namespace GhostSharp.Enums
{
    /// <summary>
    /// Fields representing a Tag
    /// </summary>
    [Flags]
    public enum TagFields
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        Id = 1,

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        Name = 2,

        /// <summary>
        /// Slug
        /// </summary>
        [JsonProperty("slug")]
        Slug = 4,

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("description")]
        Description = 8,

        /// <summary>
        /// Feature Image
        /// </summary>
        [JsonProperty("feature_image")]
        FeatureImage = 16,

        /// <summary>
        /// Visibility
        /// </summary>
        [JsonProperty("visibility")]
        Visibility = 32,

        /// <summary>
        /// Meta Title
        /// </summary>
        [JsonProperty("meta_title")]
        MetaTitle = 64,

        /// <summary>
        /// Meta Description
        /// </summary>
        [JsonProperty("meta_description")]
        MetaDescription = 128,

        /// <summary>
        /// URL
        /// </summary>
        [JsonProperty("url")]
        Url = 256,

        /// <summary>
        /// Code Injected into Header
        /// </summary>
        [JsonProperty("codeinjection_head")]
        CodeInjectionHead = 512,

        /// <summary>
        /// Code Injected into Footer
        /// </summary>
        [JsonProperty("codeinjection_foot")]
        CodeInjectionFoot = 1024,

        /// <summary>
        /// Facebook Card Image
        /// </summary>
        [JsonProperty("og_image")]
        OgImage = 2048,

        /// <summary>
        /// Facebook Card Title
        /// </summary>
        [JsonProperty("og_title")]
        OgTitle = 4096,

        /// <summary>
        /// Facebook Card Description
        /// </summary>
        [JsonProperty("og_description")]
        OgDescription = 8192,

        /// <summary>
        /// Twitter Card Image
        /// </summary>
        [JsonProperty("twitter_image")]
        TwitterImage = 16384,

        /// <summary>
        /// Twitter Card Title
        /// </summary>
        [JsonProperty("twitter_title")]
        TwitterTitle = 32768,

        /// <summary>
        /// Twitter Card Description
        /// </summary>
        [JsonProperty("twitter_description")]
        TwitterDescription = 65536,

        /// <summary>
        /// Canonical URL
        /// </summary>
        [JsonProperty("canonical_url")]
        CanonicalUrl = 131072,

        /// <summary>
        /// Accent Color
        /// </summary>
        [JsonProperty("accent_color")]
        AccentColor = 262144,
    }
}
