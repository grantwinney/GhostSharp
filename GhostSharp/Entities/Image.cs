using Newtonsoft.Json;

namespace GhostSharp.Entities
{
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
}
