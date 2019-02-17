using GhostSharp.Attributes;
using System;

namespace GhostSharp.Enums
{
    [Flags]
    public enum PostFields
    {
        [GhostField("id")]
        Id = 1,

        [GhostField("page")]
        Page = 2,

        [GhostField("uuid")]
        Uuid = 4,

        [GhostField("title")]
        Title = 8,

        [GhostField("slug")]
        Slug = 16,

        [GhostField("html")]
        Html = 32,

        [GhostField("comment_id")]
        CommentId = 64,

        [GhostField("feature_image")]
        FeatureImage = 128,

        [GhostField("featured")]
        Featured = 256,

        [GhostField("meta_title")]
        MetaTitle = 512,

        [GhostField("meta_description")]
        MetaDescription = 1024,

        [GhostField("created_at")]
        CreatedAt = 2048,

        [GhostField("updated_at")]
        UpdatedAt = 4096,

        [GhostField("published_at")]
        PublishedAt = 8192,

        [GhostField("custom_excerpt")]
        CustomExcerpt = 16384,

        [GhostField("codeinjection_head")]
        CodeInjectionHead = 32768,

        [GhostField("codeinjection_foot")]
        CodeInjectionFoot = 65536,

        [GhostField("og_image")]
        OgImage = 131072,

        [GhostField("og_title")]
        OgTitle = 262144,

        [GhostField("og_description")]
        OgDescription = 524288,

        [GhostField("twitter_image")]
        TwitterImage = 1048576,

        [GhostField("twitter_title")]
        TwitterTitle = 2097152,

        [GhostField("twitter_description")]
        TwitterDescription = 4194304,

        [GhostField("custom_template")]
        CustomTemplate = 8388608,

        [GhostField("primary_author")]
        PrimaryAuthor = 16777216,

        [GhostField("primary_tag")]
        PrimaryTag = 33554432,

        [GhostField("url")]
        Url = 67108864,

        [GhostField("excerpt")]
        Excerpt = 134217728
    }
}
