using Newtonsoft.Json;

namespace GhostSharp.Enums
{
    /// <summary>
    /// The purpose of an image (affects validation).
    /// </summary>
    public enum ImagePurpose
    {
        /// <summary>
        /// Image (JPEG, GIF, PNG or SVG)
        /// </summary>
        [JsonProperty("image")]
        Image,

        /// <summary>
        /// Profile Image (JPEG, GIF, PNG or SVG), must be square
        /// </summary>
        [JsonProperty("profile_image")]
        ProfileImage,

        /// <summary>
        /// Icon (ICO or PNG)
        /// </summary>
        [JsonProperty("icon")]
        Icon
    }
}
