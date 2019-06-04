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
}
