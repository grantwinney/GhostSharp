using Newtonsoft.Json;
using System;

namespace GhostSharp.Enums
{
    /// <summary>
    /// Fields representing an Author
    /// </summary>
    [Flags]
    public enum AuthorFields
    {
        /// <summary>
        /// Id
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
        /// Profile Image
        /// </summary>
        [JsonProperty("profile_image")]
        ProfileImage = 8,

        /// <summary>
        /// Cover Image
        /// </summary>
        [JsonProperty("cover_image")]
        CoverImage = 16,

        /// <summary>
        /// Biography
        /// </summary>
        [JsonProperty("bio")]
        Bio = 32,

        /// <summary>
        /// Website
        /// </summary>
        [JsonProperty("website")]
        Website = 64,

        /// <summary>
        /// Location
        /// </summary>
        [JsonProperty("location")]
        Location = 128,

        /// <summary>
        /// Facebook
        /// </summary>
        [JsonProperty("facebook")]
        Facebook = 256,

        /// <summary>
        /// Twitter
        /// </summary>
        [JsonProperty("twitter")]
        Twitter = 512,

        /// <summary>
        /// Meta Title
        /// </summary>
        [JsonProperty("meta_title")]
        MetaTitle = 1024,

        /// <summary>
        /// Meta Description
        /// </summary>
        [JsonProperty("meta_description")]
        MetaDescription = 2048,

        /// <summary>
        /// Profile URL
        /// </summary>
        [JsonProperty("url")]
        Url = 4096,

        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty("email")]
        Email = 8192,

        /// <summary>
        /// Accessibility
        /// </summary>
        [JsonProperty("accessibility")]
        Accessibility = 16384,

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        Status = 32768,

        /// <summary>
        /// Tour
        /// </summary>
        [JsonProperty("tour")]
        Tour = 65536,

        /// <summary>
        /// Last Seen
        /// </summary>
        [JsonProperty("last_seen")]
        LastSeen = 131072,

        /// <summary>
        /// Created At
        /// </summary>
        [JsonProperty("created_at")]
        CreatedAt = 262144,

        /// <summary>
        /// Updated At
        /// </summary>
        [JsonProperty("updated_at")]
        UpdatedAt = 524288,

        /// <summary>
        /// Roles
        /// </summary>
        [JsonProperty("roles")]
        Roles = 1048576
    }
}
