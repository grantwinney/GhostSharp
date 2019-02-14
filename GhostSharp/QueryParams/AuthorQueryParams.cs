namespace GhostSharp.QueryParams
{
    public class AuthorQueryParams
    {
        /// <summary>
        /// How many authors to retrieve, use "all" to retrieve all authors. (default is 15)
        /// </summary>
        /// <value>The number of tags to retrieve, or "all".</value>
        public int Limit { get; set; }

        /// <summary>
        /// Which page of paginated results to retrieve. (default is 1)
        /// </summary>
        /// <value>The page of results to retrieve.</value>
        public int Page { get; set; }

        /// <summary>
        /// Which field to order by and direction of ordering (asc or desc)
        /// </summary>
        /// <value>The field to order by and direction. (default is "last_login desc")</value>
        /// <example>last_login desc</example>
        public string Order { get; set; }

        /// <summary>
        /// count.posts (I have no idea what this is for; not documented)
        /// </summary>
        public string Include { get; set; }

        /// <summary>
        /// Choose which resource fields you want to retrieve values for.
        /// </summary>
        /// <value>The resource fields to retrieve values for.</value>
        public string Fields { get; set; }

        /// <summary>
        /// Use Ghost Query Language to build complex query.
        /// (warning: their implementation seems buggy, returning "Error parsing filter" when combining rules)
        /// </summary>
        /// <value>A query using the Ghost Query Language.</value>
        /// <remarks>
        /// Ghost Query Language: https://github.com/TryGhost/GQL
        /// Filters with multiple rules: https://api.ghost.org/docs/filter#filters-with-multiple-rules
        /// </remarks>
        public string Filter { get; set; }
    }
}
