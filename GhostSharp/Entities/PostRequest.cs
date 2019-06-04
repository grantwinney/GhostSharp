using GhostSharp.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Request sent to Ghost
    /// </summary>
    public class PostRequest
    {
        /// <summary>
        /// Collection of posts.
        /// </summary>
        [JsonProperty("posts")]
        [UpdatableField]
        public List<Post> Posts { get; set; }
    }
}
