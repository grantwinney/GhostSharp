using System.Collections.Generic;

namespace GhostSharp.Entities
{
    public class PostResult
    {
        public List<Post> Posts { get; set; }    
    }

    public class Post
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Html { get; set; }
        public string FeatureImage { get; set; }
        public bool Featured { get; set; }
        public bool Page { get; set; }
        public string Status { get; set; }
        public string Locale { get; set; }
        public string Visibility { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string PublishedAt { get; set; }
        public string PublishedBy { get; set; }
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
        public string Author { get; set; }
        public string PrimaryTag { get; set; }
        public string Url { get; set; }
        public string CommentId { get; set; }
    }
}
