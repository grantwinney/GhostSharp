namespace GhostSharp.QueryParams
{
    /// <summary>
    /// Query parameters that modify the payload that's returned, including
    /// limiting to or filtering by certain fields, including extra data, etc.
    /// </summary>
    public class TierQueryParams
    {
        /// <summary>
        /// Specify whether or not to include the monthly price for the tier
        /// </summary>
        /// <value>True to include the monthly price; otherwise False</value>
        public bool IncludeMonthlyPrice { get; set; }

        /// <summary>
        /// Specify whether or not to include the yearly price for the tier
        /// </summary>
        /// <value>True to include the yearly price; otherwise False</value>
        public bool IncludeYearlyPrice { get; set; }

        /// <summary>
        /// Specify whether or not to include the benefits of the tier
        /// </summary>
        /// <value>True to include the benefits; otherwise False</value>
        public bool IncludeBenefits { get; set; }
    }
}
