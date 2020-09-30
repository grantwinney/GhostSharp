using GhostSharp.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Request sent to Ghost
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// Collection of pages.
        /// </summary>
        [JsonProperty("pages")]
        [RequiredForUpdate]
        public List<Post> Pages { get; set; }
    }
}
