using GhostSharp.Attributes;
using System;

namespace GhostSharp.Enums
{
    /// <summary>
    /// Fields representing a Tag
    /// </summary>
    [Flags]
    public enum TagFields
    {
        /// <summary>
        /// ID
        /// </summary>
        [GhostField("id")]
        Id = 1,

        /// <summary>
        /// Name
        /// </summary>
        [GhostField("name")]
        Name = 2,

        /// <summary>
        /// Slug
        /// </summary>
        [GhostField("slug")]
        Slug = 4,

        /// <summary>
        /// Description
        /// </summary>
        [GhostField("description")]
        Description = 8,

        /// <summary>
        /// Feature Image
        /// </summary>
        [GhostField("feature_image")]
        FeatureImage = 16,

        /// <summary>
        /// Visibility
        /// </summary>
        [GhostField("visibility")]
        Visibility = 32,

        /// <summary>
        /// Meta Title
        /// </summary>
        [GhostField("meta_title")]
        MetaTitle = 64,

        /// <summary>
        /// Meta Description
        /// </summary>
        [GhostField("meta_description")]
        MetaDescription = 128,

        /// <summary>
        /// URL
        /// </summary>
        [GhostField("url")]
        Url = 256
    }
}
