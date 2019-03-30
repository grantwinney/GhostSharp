using GhostSharp.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing an image
    /// </summary>
    public class ImageResponse
    {
        /// <summary>
        /// List of images
        /// </summary>
        [JsonProperty("images")]
        public List<Image> Images { get; set; }
    }

    /// <summary>
    /// Represents an image
    /// </summary>
    public class Image
    {
        /// <summary>
        /// The location from which the image can be fetched
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Reference or identifier for the image, if one was provided
        /// </summary>
        /// <remarks>
        /// A reference or identifier for the image, e.g. the original filename and path.
        /// Will be returned as-is in the API response, making it useful for finding & replacing local image paths after uploads.
        /// </remarks>
        [JsonProperty("ref")]
        public string Reference { get; set; }
    }

    public class ImageRequest
    {
        /// <summary>
        /// The image data that you want to upload
        /// </summary>
        public byte[] File { get; set; }

        /// <summary>
        /// Intended use for the image (affects validation)
        /// </summary>
        [JsonProperty("purpose")]
        public ImagePurpose Purpose { get; set; }

        /// <summary>
        /// Reference or identifier for the image, if one was provided
        /// </summary>
        /// <remarks>
        /// A reference or identifier for the image, e.g. the original filename and path.
        /// Will be returned as-is in the API response, making it useful for finding & replacing local image paths after uploads.
        /// </remarks>
        [JsonProperty("ref")]
        public string Reference { get; set; }
    }
}
