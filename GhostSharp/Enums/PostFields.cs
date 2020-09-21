using Newtonsoft.Json;
using System;

namespace GhostSharp.Enums
{
    /// <summary>
    /// Fields representing a Post
    /// </summary>
    [Flags]
    public enum PostFields
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        Id = 1,

        /// <summary>
        /// Page (true if Page, false if Post)
        /// </summary>
        [JsonProperty("page")]
        Page = 2,

        /// <summary>
        /// UUID
        /// </summary>
        [JsonProperty("uuid")]
        Uuid = 4,

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
        Title = 8,

        /// <summary>
        /// Slug
        /// </summary>
        [JsonProperty("slug")]
        Slug = 16,

        /// <summary>
        /// Post, formatted in HTML
        /// </summary>
        [JsonProperty("html")]
        Html = 32,

        /// <summary>
        /// Comment ID
        /// </summary>
        [JsonProperty("comment_id")]
        CommentId = 64,

        /// <summary>
        /// Feature Image
        /// </summary>
        [JsonProperty("feature_image")]
        FeatureImage = 128,

        /// <summary>
        /// Featured (true if featured)
        /// </summary>
        [JsonProperty("featured")]
        Featured = 256,

        /// <summary>
        /// Meta Title
        /// </summary>
        [JsonProperty("meta_title")]
        MetaTitle = 512,

        /// <summary>
        /// Meta Description
        /// </summary>
        [JsonProperty("meta_description")]
        MetaDescription = 1024,

        /// <summary>
        /// Created At
        /// </summary>
        [JsonProperty("created_at")]
        CreatedAt = 2048,

        /// <summary>
        /// Updated At
        /// </summary>
        [JsonProperty("updated_at")]
        UpdatedAt = 4096,

        /// <summary>
        /// Published At
        /// </summary>
        [JsonProperty("published_at")]
        PublishedAt = 8192,

        /// <summary>
        /// Custom Excerpt
        /// </summary>
        [JsonProperty("custom_excerpt")]
        CustomExcerpt = 16384,

        /// <summary>
        /// Code Injected into Header
        /// </summary>
        [JsonProperty("codeinjection_head")]
        CodeInjectionHead = 32768,

        /// <summary>
        /// Code Injected into Footer
        /// </summary>
        [JsonProperty("codeinjection_foot")]
        CodeInjectionFoot = 65536,

        /// <summary>
        /// Facebook Card Image
        /// </summary>
        [JsonProperty("og_image")]
        OgImage = 131072,

        /// <summary>
        /// Facebook Card Title
        /// </summary>
        [JsonProperty("og_title")]
        OgTitle = 262144,

        /// <summary>
        /// Facebook Card Description
        /// </summary>
        [JsonProperty("og_description")]
        OgDescription = 524288,

        /// <summary>
        /// Twitter Card Image
        /// </summary>
        [JsonProperty("twitter_image")]
        TwitterImage = 1048576,

        /// <summary>
        /// Twitter Card Title
        /// </summary>
        [JsonProperty("twitter_title")]
        TwitterTitle = 2097152,

        /// <summary>
        /// Twitter Card Description
        /// </summary>
        [JsonProperty("twitter_description")]
        TwitterDescription = 4194304,

        /// <summary>
        /// Custom Template
        /// </summary>
        [JsonProperty("custom_template")]
        CustomTemplate = 8388608,

        /// <summary>
        /// Primary Author
        /// </summary>
        [JsonProperty("primary_author")]
        PrimaryAuthor = 16777216,

        /// <summary>
        /// Primary Tag
        /// </summary>
        [JsonProperty("primary_tag")]
        PrimaryTag = 33554432,

        /// <summary>
        /// URL
        /// </summary>
        [JsonProperty("url")]
        Url = 67108864,

        /// <summary>
        /// Excerpt
        /// </summary>
        [JsonProperty("excerpt")]
        Excerpt = 134217728,

        /// <summary>
        /// Reading Time in minutes
        /// </summary>
        [JsonProperty("reading_time")]
        ReadingTime = 268435456,

        /// <summary>
        /// Canonical URL
        /// </summary>
        [JsonProperty("canonical_url")]
        CanonicalUrl = 536870912,

        /// <summary>
        /// Status (published, draft, scheduled) - If 'scheduled', a PublishedAt date is required
        /// </summary>
        [JsonProperty("status")]
        Status = 1073741824,
    }
}
