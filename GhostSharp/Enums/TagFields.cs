using GhostSharp.Attributes;
using System;

namespace GhostSharp.Enums
{
    [Flags]
    public enum TagFields
    {
        [GhostField("id")]
        Id = 1,

        [GhostField("name")]
        Name = 2,

        [GhostField("slug")]
        Slug = 4,

        [GhostField("description")]
        Description = 8,

        [GhostField("feature_image")]
        FeatureImage = 16,

        [GhostField("visibility")]
        Visibility = 32,

        [GhostField("meta_title")]
        MetaTitle = 64,

        [GhostField("meta_description")]
        MetaDescription = 128,

        [GhostField("url")]
        Url = 256
    }
}
