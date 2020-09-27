using GhostSharp.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Represents a Page.
    /// </summary>
    /// <remarks>
    /// There are some constraints that exist on these fields, defined here:
    /// https://github.com/TryGhost/Ghost/blob/master/core/server/data/schema/schema.js
    /// </remarks>
    public class Page
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; private set; }

        /// <summary>
        /// IsPage (true if Page, false if Post)
        /// </summary>
        [JsonIgnore]
        public bool IsPage => true;

        /// <summary>
        /// UUID
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; private set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
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
        public string MobileDoc { get; set; }

        /// <summary>
        /// Post, formatted in HTML (if specified, then MobileDoc is ignored)
        /// </summary>
        [JsonProperty("html")]
        public string Html { get; set; }

        /// <summary>
        /// Post, formatted in Plain Text
        /// </summary>
        [JsonProperty("plaintext")]
        public string PlainText { get; private set; }

        /// <summary>
        /// Comment ID
        /// </summary>
        [JsonProperty("comment_id")]
        public string CommentId { get; private set; }

        /// <summary>
        /// Feature Image
        /// </summary>
        [JsonProperty("feature_image")]
        public string FeatureImage { get; set; }

        /// <summary>
        /// Featured (true if featured)
        /// </summary>
        [JsonProperty("featured")]
        public bool? Featured { get; set; }

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
        /// Created At
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// Updated At
        /// </summary>
        [JsonProperty("updated_at")]
        [RequiredForUpdate]
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// Published At (UTC)
        /// </summary>
        [JsonProperty("published_at")]
        public DateTime? PublishedAt { get; set; }

        /// <summary>
        /// Custom Excerpt
        /// </summary>
        [JsonProperty("custom_excerpt")]
        public string CustomExcerpt { get; set; }

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
        /// Custom Template
        /// </summary>
        [JsonProperty("custom_template")]
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
        public string Url { get; private set; }

        /// <summary>
        /// Excerpt
        /// </summary>
        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }

        /// <summary>
        /// Reading time in minutes
        /// </summary>
        [JsonProperty("reading_time")]
        public int? ReadingTime { get; private set; }

        /// <summary>
        /// Canonical URL
        /// </summary>
        [JsonProperty("canonical_url")]
        public string CanonicalUrl { get; set; }

        /// <summary>
        /// Status (published, draft, scheduled) - If 'scheduled', a PublishedAt date is required
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Visibility (public, members, paid)
        /// </summary>
        /// <see cref="https://ghost.org/docs/members/content-visibility/#visibility"/>
        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        /// <summary>
        /// Send Email When Published
        /// </summary>
        /// <remarks>
        /// This field seems to be ignored, even if the status is changed to scheduled.
        /// </remarks>
        [JsonProperty("send_email_when_published")]
        public bool SendEmailWhenPublished { get; private set; }

        /// <summary>
        /// Email Subject
        /// </summary>
        [JsonProperty("email_subject")]
        public string EmailSubject { get; set; }

        /// <summary>
        /// Access (true if members is disabled; otherwise, set to currently logged in members' access)
        /// </summary>
        /// <remarks>
        /// This value seems to always be null on POST.
        /// </remarks>
        /// <see cref="https://github.com/TryGhost/Ghost/commit/289c1b3e8a5c03b868dde1a76f62661197e302d4"/>
        [JsonProperty("access")]
        public bool? Access { get; private set; }
    }
}
