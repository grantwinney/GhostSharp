using System;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing posts and any meta data
    /// </summary>
    public class PostResponse
    {
        /// <summary>
        /// Collection of posts.
        /// </summary>
        public List<Post> Posts { get; set; }

        /// <summary>
        /// Meta data regarding the response.
        /// </summary>
        public Meta Meta { get; set; }
    }

    /// <summary>
    /// Represents a Post.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Page (true if Page, false if Post)
        /// </summary>
        public bool? Page { get; set; }

        /// <summary>
        /// UUID
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Slug
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Post, formatted in Mobile Doc
        /// </summary>
        public string MobileDoc { get; set; }

        /// <summary>
        /// Post, formatted in HTML
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Post, formatted in Plain Text
        /// </summary>
        public string PlainText { get; set; }

        /// <summary>
        /// Comment ID
        /// </summary>
        public string CommentId { get; set; }

        /// <summary>
        /// Feature Image
        /// </summary>
        public string FeatureImage { get; set; }

        /// <summary>
        /// Featured (true if featured)
        /// </summary>
        public bool? Featured { get; set; }

        /// <summary>
        /// Meta Title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Meta Description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Updated At
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
   
        /// <summary>
        /// Published At
        /// </summary>
        public DateTime? PublishedAt { get; set; }

        /// <summary>
        /// Custom Excerpt
        /// </summary>
        public string CustomExcerpt { get; set; }

        /// <summary>
        /// Code Injected into Header
        /// </summary>
        public string CodeInjectionHead { get; set; }

        /// <summary>
        /// Code Injected into Footer
        /// </summary>
        public string CodeInjectionFoot { get; set; }

        /// <summary>
        /// Facebook Card Image
        /// </summary>
        public string OgImage { get; set; }

        /// <summary>
        /// Facebook Card Title
        /// </summary>
        public string OgTitle { get; set; }

        /// <summary>
        /// Facebook Card Description
        /// </summary>
        public string OgDescription { get; set; }

        /// <summary>
        /// Twitter Card Image
        /// </summary>
        public string TwitterImage { get; set; }

        /// <summary>
        /// Twitter Card Title
        /// </summary>
        public string TwitterTitle { get; set; }

        /// <summary>
        /// Twitter Card Description
        /// </summary>
        public string TwitterDescription { get; set; }

        /// <summary>
        /// Custom Template
        /// </summary>
        public string CustomTemplate { get; set; }

        /// <summary>
        /// List of Authors
        /// </summary>
        public List<Author> Authors { get; set; }

        /// <summary>
        /// Primary Author
        /// </summary>
        public Author PrimaryAuthor { get; set; }

        /// <summary>
        /// List of Tags
        /// </summary>
        public List<Tag> Tags { get; set; }
    
        /// <summary>
        /// Primary Tag
        /// </summary>
        public Tag PrimaryTag { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Excerpt
        /// </summary>
        public string Excerpt { get; set; }
    }
}
