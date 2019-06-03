namespace GhostSharp.Enums
{
    public enum ImageType
    {
        /// <summary>
        /// Invalid. Serves as a default for when no value is set.
        /// </summary>
        Unknown,

        /// <summary>
        /// Supported for 'image' and 'profile_image'
        /// </summary>
        GIF,

        /// <summary>
        /// Supported for 'icon'
        /// </summary>
        ICO,

        /// <summary>
        /// Supported for 'image' and 'profile_image'
        /// </summary>
        JPEG,

        /// <summary>
        /// Supported for 'image', 'profile_image', and 'icon'
        /// </summary>
        PNG,

        /// <summary>
        /// Supported for 'image' and 'profile_image'
        /// </summary>
        SVG
    }
}
