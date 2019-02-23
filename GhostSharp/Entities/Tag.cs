using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing tags and any meta data.
    /// </summary>
    public class TagResponse
    {
        /// <summary>
        /// Collection of tags.
        /// </summary>
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Meta data regarding the response.
        /// </summary>
        public Meta Meta { get; set; }
    }

    /// <summary>
    /// Represents a Tag.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Slug
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Feature Image
        /// </summary>
        public string FeatureImage { get; set; }

        /// <summary>
        /// Visibility
        /// </summary>
        public string Visibility { get; set; }

        /// <summary>
        /// Meta Title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Meta Description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Post Count
        /// </summary>
        public Count Count { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }
    }
}
