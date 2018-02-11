using System;
namespace GhostSharp.Entities
{
    public class Meta
    {
        public Pagination Pagination { get; set; }
    }

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
