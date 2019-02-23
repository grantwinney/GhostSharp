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
        public List<Page> Pages { get; set; }

        /// <summary>
        /// Meta data regarding the response.
        /// </summary>
        public Meta Meta { get; set; }
    }

    /// <summary>
    /// Represents a Page.
    /// </summary>
    public class Page : Post
    {
    }
}
