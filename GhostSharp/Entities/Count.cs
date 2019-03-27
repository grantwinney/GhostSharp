using Newtonsoft.Json;

namespace GhostSharp.Entities
{
    /// <summary>
    /// The post count associated with an author or tag.
    /// </summary>
    public class Count
    {
        /// <summary>
        /// The post count associated with an author or tag.
        /// </summary>
        [JsonProperty("posts")]
        public int Posts { get; set; }
    }
}
