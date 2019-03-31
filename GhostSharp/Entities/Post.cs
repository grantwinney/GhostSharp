using GhostSharp.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response from Ghost, representing posts and any meta data
    /// </summary>
    public class PostResponse
    {
        /// <summary>
        /// Collection of posts.
        /// </summary>
        [JsonProperty("posts")]
        public List<Post> Posts { get; set; }

        /// <summary>
        /// Meta data regarding the response.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }

    /// <summary>
    /// Request sent to Ghost
    /// </summary>
    public class PostRequest
    {
        /// <summary>
        /// Collection of posts.
        /// </summary>
        [JsonProperty("posts")]
        [UpdatableField]
        public List<Post> Posts { get; set; }
    }

    /// <summary>
    /// Represents a Post.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// ID (Update)
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Page (true if Page, false if Post)
        /// </summary>
        [JsonProperty("page")]
        public bool? Page { get; set; }

        /// <summary>
        /// UUID
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
        [UpdatableField]
        public string Title { get; set; }

        /// <summary>
        /// Slug
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Post, formatted in Mobile Doc
        /// </summary>
        [JsonProperty("mobiledoc")]
        [UpdatableField]
        public string MobileDoc { get; set; }

        /// <summary>
        /// Post, formatted in HTML
        /// </summary>
        [JsonProperty("html")]
        [UpdatableField]
        public string Html { get; set; }

        /// <summary>
        /// Post, formatted in Plain Text
        /// </summary>
        [JsonProperty("plaintext")]
        [UpdatableField]
        public string PlainText { get; set; }

        /// <summary>
        /// Comment ID
        /// </summary>
        [JsonProperty("comment_id")]
        public string CommentId { get; set; }

        /// <summary>
        /// Feature Image
        /// </summary>
        [JsonProperty("feature_image")]
        [UpdatableField]
        public string FeatureImage { get; set; }

        /// <summary>
        /// Featured (true if featured)
        /// </summary>
        [JsonProperty("featured")]
        [UpdatableField]
        public bool? Featured { get; set; }

        /// <summary>
        /// Meta Title
        /// </summary>
        [JsonProperty("meta_title")]
        [UpdatableField]
        public string MetaTitle { get; set; }

        /// <summary>
        /// Meta Description
        /// </summary>
        [JsonProperty("meta_description")]
        [UpdatableField]
        public string MetaDescription { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Updated At
        /// </summary>
        [JsonProperty("updated_at")]
        [UpdatableField]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Published At
        /// </summary>
        [JsonProperty("published_at")]
        [UpdatableField]
        public DateTime? PublishedAt { get; set; }

        /// <summary>
        /// Custom Excerpt
        /// </summary>
        [JsonProperty("custom_excerpt")]
        [UpdatableField]
        public string CustomExcerpt { get; set; }

        /// <summary>
        /// Code Injected into Header
        /// </summary>
        [JsonProperty("codeinjection_head")]
        [UpdatableField]
        public string CodeInjectionHead { get; set; }

        /// <summary>
        /// Code Injected into Footer
        /// </summary>
        [JsonProperty("codeinjection_foot")]
        [UpdatableField]
        public string CodeInjectionFoot { get; set; }

        /// <summary>
        /// Facebook Card Image
        /// </summary>
        [JsonProperty("og_image")]
        [UpdatableField]
        public string OgImage { get; set; }

        /// <summary>
        /// Facebook Card Title
        /// </summary>
        [JsonProperty("og_title")]
        [UpdatableField]
        public string OgTitle { get; set; }

        /// <summary>
        /// Facebook Card Description
        /// </summary>
        [JsonProperty("og_description")]
        [UpdatableField]
        public string OgDescription { get; set; }

        /// <summary>
        /// Twitter Card Image
        /// </summary>
        [JsonProperty("twitter_image")]
        [UpdatableField]
        public string TwitterImage { get; set; }

        /// <summary>
        /// Twitter Card Title
        /// </summary>
        [JsonProperty("twitter_title")]
        [UpdatableField]
        public string TwitterTitle { get; set; }

        /// <summary>
        /// Twitter Card Description
        /// </summary>
        [JsonProperty("twitter_description")]
        [UpdatableField]
        public string TwitterDescription { get; set; }

        /// <summary>
        /// Custom Template
        /// </summary>
        [JsonProperty("custom_template")]
        [UpdatableField]
        public string CustomTemplate { get; set; }

        /// <summary>
        /// List of Authors
        /// </summary>
        [JsonProperty("authors")]
        public List<Author> Authors { get; set; }

        /// <summary>
        /// Primary Author
        /// </summary>
        [JsonProperty("primary_author")]
        public Author PrimaryAuthor { get; set; }

        /// <summary>
        /// List of Tags
        /// </summary>
        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Primary Tag
        /// </summary>
        [JsonProperty("primary_tag")]
        public Tag PrimaryTag { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        [JsonProperty("url")]
        [UpdatableField]
        public string Url { get; set; }

        /// <summary>
        /// Canonical URL
        /// </summary>
        [JsonProperty("canonical_url")]
        [UpdatableField]
        public string CanonicalUrl { get; set; }

        /// <summary>
        /// Excerpt
        /// </summary>
        [JsonProperty("excerpt")]
        [UpdatableField]
        public string Excerpt { get; set; }

        /// <summary>
        /// Status (published, draft)
        /// </summary>
        [JsonProperty("status")]
        [UpdatableField]
        public string Status { get; set; }

        public static IEnumerable<PropertyInfo> UpdatableProperties =>
            typeof(Post).GetProperties().Where(x => x.GetCustomAttribute<UpdatableFieldAttribute>() != null);
    }
}
