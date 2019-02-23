using GhostSharp.Attributes;
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
        [GhostField("id")]
        Id = 1,

        /// <summary>
        /// Page (true if Page, false if Post)
        /// </summary>
        [GhostField("page")]
        Page = 2,

        /// <summary>
        /// UUID
        /// </summary>
        [GhostField("uuid")]
        Uuid = 4,

        /// <summary>
        /// Title
        /// </summary>
        [GhostField("title")]
        Title = 8,

        /// <summary>
        /// Slug
        /// </summary>
        [GhostField("slug")]
        Slug = 16,

        /// <summary>
        /// Post, formatted in HTML
        /// </summary>
        [GhostField("html")]
        Html = 32,

        /// <summary>
        /// Comment ID
        /// </summary>
        [GhostField("comment_id")]
        CommentId = 64,

        /// <summary>
        /// Feature Image
        /// </summary>
        [GhostField("feature_image")]
        FeatureImage = 128,

        /// <summary>
        /// Featured (true if featured)
        /// </summary>
        [GhostField("featured")]
        Featured = 256,

        /// <summary>
        /// Meta Title
        /// </summary>
        [GhostField("meta_title")]
        MetaTitle = 512,

        /// <summary>
        /// Meta Description
        /// </summary>
        [GhostField("meta_description")]
        MetaDescription = 1024,

        /// <summary>
        /// Created At
        /// </summary>
        [GhostField("created_at")]
        CreatedAt = 2048,

        /// <summary>
        /// Updated At
        /// </summary>
        [GhostField("updated_at")]
        UpdatedAt = 4096,

        /// <summary>
        /// Published At
        /// </summary>
        [GhostField("published_at")]
        PublishedAt = 8192,

        /// <summary>
        /// Custom Excerpt
        /// </summary>
        [GhostField("custom_excerpt")]
        CustomExcerpt = 16384,

        /// <summary>
        /// Code Injected into Header
        /// </summary>
        [GhostField("codeinjection_head")]
        CodeInjectionHead = 32768,

        /// <summary>
        /// Code Injected into Footer
        /// </summary>
        [GhostField("codeinjection_foot")]
        CodeInjectionFoot = 65536,

        /// <summary>
        /// Facebook Card Image
        /// </summary>
        [GhostField("og_image")]
        OgImage = 131072,

        /// <summary>
        /// Facebook Card Title
        /// </summary>
        [GhostField("og_title")]
        OgTitle = 262144,

        /// <summary>
        /// Facebook Card Description
        /// </summary>
        [GhostField("og_description")]
        OgDescription = 524288,

        /// <summary>
        /// Twitter Card Image
        /// </summary>
        [GhostField("twitter_image")]
        TwitterImage = 1048576,

        /// <summary>
        /// Twitter Card Title
        /// </summary>
        [GhostField("twitter_title")]
        TwitterTitle = 2097152,

        /// <summary>
        /// Twitter Card Description
        /// </summary>
        [GhostField("twitter_description")]
        TwitterDescription = 4194304,

        /// <summary>
        /// Custom Template
        /// </summary>
        [GhostField("custom_template")]
        CustomTemplate = 8388608,

        /// <summary>
        /// Primary Author
        /// </summary>
        [GhostField("primary_author")]
        PrimaryAuthor = 16777216,

        /// <summary>
        /// Primary Tag
        /// </summary>
        [GhostField("primary_tag")]
        PrimaryTag = 33554432,

        /// <summary>
        /// URL
        /// </summary>
        [GhostField("url")]
        Url = 67108864,

        /// <summary>
        /// Excerpt
        /// </summary>
        [GhostField("excerpt")]
        Excerpt = 134217728
    }
}
