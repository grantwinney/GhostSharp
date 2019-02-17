using System;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    public class PostResponse
    {
        public List<Post> Posts { get; set; }
        public Meta Meta { get; set; }
    }

    public class Post
    {
        public string Id { get; set; }
        public bool Page { get; set; }
        public string Uuid { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string MobileDoc { get; set; }
        public string Html { get; set; }
        public string PlainText { get; set; }
        public string CommentId { get; set; }
        public string FeatureImage { get; set; }
        public bool Featured { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public string CustomExcerpt { get; set; }
        public string CodeInjectionHead { get; set; }
        public string CodeInjectionFoot { get; set; }
        public string OgImage { get; set; }
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
        public string TwitterImage { get; set; }
        public string TwitterTitle { get; set; }
        public string TwitterDescription { get; set; }
        public string CustomTemplate { get; set; }
        public List<Author> Authors { get; set; }
        public Author PrimaryAuthor { get; set; }
        public List<Tag> Tags { get; set; }
        public Tag PrimaryTag { get; set; }
        public string Url { get; set; }
        public string Excerpt { get; set; }
    }
}
