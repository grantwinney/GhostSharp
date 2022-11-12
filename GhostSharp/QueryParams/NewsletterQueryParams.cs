namespace GhostSharp.QueryParams
{
    /// <summary>
    /// Query parameters that modify the payload that's returned.
    /// </summary>
    public class NewsletterQueryParams
    {

        /// <summary>
        /// Return all newsletters (no limit); if set to True, then Limit is ignored.
        /// </summary>
        public bool NoLimit { get; set; }

        /// <summary>
        /// The number of newsletters to retrieve.
        /// </summary>
        public int Limit { get; set; }
    }
}
