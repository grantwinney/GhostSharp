using GhostSharp.Attributes;
using System;

namespace GhostSharp.Enums
{
    /// <summary>
    /// Fields representing an Author
    /// </summary>
    [Flags]
    public enum AuthorFields
    {
        /// <summary>
        /// Id
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
        /// Profile Image
        /// </summary>
        [GhostField("profile_image")]
        ProfileImage = 8,

        /// <summary>
        /// Cover Image
        /// </summary>
        [GhostField("cover_image")]
        CoverImage = 16,

        /// <summary>
        /// Biography
        /// </summary>
        [GhostField("bio")]
        Bio = 32,

        /// <summary>
        /// Website
        /// </summary>
        [GhostField("website")]
        Website = 64,

        /// <summary>
        /// Location
        /// </summary>
        [GhostField("location")]
        Location = 128,

        /// <summary>
        /// Facebook
        /// </summary>
        [GhostField("facebook")]
        Facebook = 256,

        /// <summary>
        /// Twitter
        /// </summary>
        [GhostField("twitter")]
        Twitter = 512,

        /// <summary>
        /// Meta Title
        /// </summary>
        [GhostField("meta_title")]
        MetaTitle = 1024,

        /// <summary>
        /// Meta Description
        /// </summary>
        [GhostField("meta_description")]
        MetaDescription = 2048,

        /// <summary>
        /// Profile URL
        /// </summary>
        [GhostField("url")]
        Url = 4096
    }
}
