using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostSharp.Entities
{
    /// <summary>
    /// Response representing pages and any meta data.
    /// </summary>
    public class PageResponse
    {
        /// <summary>
        /// Collection of pages.
        /// </summary>
        [JsonProperty("pages")]
        public List<Page> Pages { get; set; }

        /// <summary>
        /// Meta data regarding the response.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
