namespace GhostSharp.Entities
{
    /// <summary>
    /// Holds information about the request, such as metadata about the 'page' of data that was requested.
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
    /// Source: https://api.ghost.org/docs/pagination
    /// </remarks>
    public class Pagination
    {
        /// <summary>
        /// Current page of result set.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Items per page.
        /// </summary>
        public int Limit { get; set; }

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
        public int Next { get; set; }

        /// <summary>
        /// Previous page number in result set.
        /// </summary>
        public int Prev { get; set; }
    }
}
