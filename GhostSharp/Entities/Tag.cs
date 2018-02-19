using System.Collections.Generic;

namespace GhostSharp.Entities
{
    public class TagResponse
    {
        public List<Tag> Tags { get; set; }
        public Meta Meta { get; set; }
    }

    public class Tag
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string FeatureImage { get; set; }
        /// <summary>
        /// Gets or sets the visibility. (public)
        /// </summary>
        /// <value>The visibility.</value>
        public string Visibility { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string Parent { get; set; }
    }
}
