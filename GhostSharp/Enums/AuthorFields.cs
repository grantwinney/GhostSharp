using GhostSharp.Attributes;
using System;

namespace GhostSharp.Enums
{
    [Flags]
    public enum AuthorFields
    {
        [GhostField("id")]
        Id = 1,

        [GhostField("name")]
        Name = 2,

        [GhostField("slug")]
        Slug = 4,

        [GhostField("profile_image")]
        ProfileImage = 8,

        [GhostField("cover_image")]
        CoverImage = 16,

        [GhostField("bio")]
        Bio = 32,

        [GhostField("website")]
        Website = 64,

        [GhostField("location")]
        Location = 128,

        [GhostField("facebook")]
        Facebook = 256,

        [GhostField("twitter")]
        Twitter = 512,

        [GhostField("meta_title")]
        MetaTitle = 1024,

        [GhostField("meta_description")]
        MetaDescription = 2048,

        [GhostField("url")]
        Url = 4096
    }
}
