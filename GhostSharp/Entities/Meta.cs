namespace GhostSharp.Entities
{
    /// <summary>
    /// Holds meta information about the request, such as the 'page' of data that was requested.
    /// </summary>
    public class Meta   
    {
        public Pagination Pagination { get; set; }
    }

    /// <summary>
    /// Represents the current 'page' of data you've retrieved, controlled by the Limit and Page values.
    /// </summary>
    /// <remarks>
    /// When fetching multiple resources from an endpoint, e.g. performing a 'browse' request
    /// the number of items returned will be limited to 15 by default.
    /// 
    /// Ghost's API currently uses page-based pagination, which is controlled via the two
    /// parameters Limit and Page. The API returns page 1 by default, and requesting page 2
    /// will send the next 15 items. Alternatively you can make a request for more or less items,
    /// so that you can get the exact data that you need, by changing the limit.
    /// 
    /// When browsing the posts, users and tags endpoints you will retrieve an array of your
    /// queried resource and a meta object that includes a pagination object.
    /// 
    /// Source: https://docs.ghost.org/api/content/#pagination
    /// </remarks>
    public class Pagination
    {
        const int DEFAULT_LIMIT = 15;

        /// <summary>
        /// Current page of result set.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Items per page. Default is 15.
        /// </summary>
        public string Limit { get; set; }

        /// <summary>
        /// Converts Limit to an integer, for convenience.
        /// </summary>
        public int LimitNumber
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Limit))
                    return DEFAULT_LIMIT;
                else if (Limit == "all")
                    return int.MaxValue;
                else
                    return int.Parse(Limit);
            }
        }

        /// <summary>
        /// Total pages in result set.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// Total items in result set.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Next page number in result set.
        /// </summary>
        public int? Next { get; set; }

        /// <summary>
        /// Previous page number in result set.
        /// </summary>
        public int? Prev { get; set; }
    }
}
