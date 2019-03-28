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
        Url = 256
    }
}
