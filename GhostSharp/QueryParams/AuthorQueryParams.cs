using GhostSharp.Enums;
using System;
using System.Collections.Generic;

namespace GhostSharp.QueryParams
{
    public class AuthorQueryParams
    {
        /// <summary>
        /// Include a count of posts associated with the author.
        /// </summary>
        public bool IncludePostCount { get; set; }

        /// <summary>
        /// Choose which resource fields you want to retrieve values for.
        /// </summary>
        /// <value>The resource fields to retrieve values for.</value>
        public AuthorFields Fields { get; set; }

        /// <summary>
        /// A filter string using NQL syntax.
        /// </summary>
        /// <example>
        /// See documentation: https://docs.ghost.org/api/content/#filtering
        /// </example>
        public string Filter { get; set; }

        /// <summary>
        /// How many authors to retrieve. (default is 15)
        /// </summary>
        /// <value>The number of tags to retrieve.</value>
        public int Limit { get; set; }

        /// <summary>
        /// Return all authors (no limit) - use carefully! If set to True, then Limit is ignored.
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
        /// <value>The field(s) to order by and direction. (default is "name ASC")</value>
        public List<Tuple<AuthorFields, OrderDirection>> Order { get; set; }
    }
}
