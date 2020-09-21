using GhostSharp.Enums;
using System;
using System.Collections.Generic;

namespace GhostSharp.QueryParams
{
    /// <summary>
    /// Query parameters that modify the payload that's returned, including
    /// limiting to or filtering by certain fields, including extra data, etc.
    /// </summary>
    public class PostQueryParams
    {
        /// <summary>
        /// Specify whether or not to include full data about the authors (default is to not include)
        /// </summary>
        /// <value>True to include the authors; otherwise False</value>
        public bool IncludeAuthors { get; set; }

        /// <summary>
        /// Specify whether or not to include tags (default is to not include)
        /// </summary>
        /// <value>True to include tags; otherwise False</value>
        public bool IncludeTags { get; set; }

        /// <summary>
        /// Limit fields returned in response.
        /// Useful for optimizing queries, but does not play well with the Include options.
        /// </summary>
        /// <value>The fields to return in the response.</value>
        public PostFields Fields { get; set; }

        /// <summary>
        /// Limit fields returned in response.
        /// Useful for optimizing queries, but does not play well with the Include options.
        /// </summary>
        /// <remarks>
        /// There are too many fields in a Post/Page object to fit in a single flags enum. Might want to rethink this...
        /// </remarks>
        /// <value>The fields to return in the response.</value>
        public PostFields2 Fields2 { get; set; }

        /// <summary>
        /// The format(s) in which to return the body of the page or post.
        /// </summary>
        public PostFormat Formats { get; set; }

        /// <summary>
        /// A filter string using NQL syntax.
        /// </summary>
        /// <example>
        /// See documentation: https://docs.ghost.org/api/content/#filtering
        /// </example>
        public string Filter { get; set; }

        /// <summary>
        /// How many posts or pages to retrieve. (default is 15)
        /// </summary>
        /// <value>The number of posts to retrieve.</value>
        public int Limit { get; set; }

        /// <summary>
        /// Return all posts or pages (no limit) - use carefully! If set to True, then Limit is ignored.
        /// </summary>
        public bool NoLimit { get; set; }

        /// <summary>
        /// Which page of paginated results to retrieve. (default is 1)
        /// </summary>
        /// <value>The page of results to retrieve.</value>
        public int Page { get; set; }

        /// <summary>
        /// A list of fields to order by and direction of ordering.
        /// </summary>
        /// <value>
        /// The field(s) to order by and direction.
        /// (default is "title ASC" for pages, and "published_at DESC" for posts)
        /// </value>
        public List<Tuple<PostFields, OrderDirection>> Order { get; set; }
    }
}
