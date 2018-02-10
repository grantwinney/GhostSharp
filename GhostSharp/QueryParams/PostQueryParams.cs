namespace GhostSharp.QueryParams
{
    public class PostQueryParams
    {
        /// <summary>
        /// Specify whether or not to include tags (default is to not include tags)
        /// </summary>
        /// <value>True to include tags; otherwise False</value>
        public bool IncludeTags { get; set; }

        /// <summary>
        /// How many posts to retrieve, use "all" to retrieve all posts. (default is 15)
        /// </summary>
        /// <value>The number of posts to retrieve, or "all".</value>
        public int Limit { get; set; }

        /// <summary>
        /// Which page of paginated results to retrieve. (default is 1)
        /// </summary>
        /// <value>The page of results to retrieve.</value>
        public int Page { get; set; }

        /// <summary>
        /// Which field to order by and direction of ordering (asc or desc)
        /// </summary>
        /// <value>The field to order by and direction.</value>
        /// <example>published_at desc</example>
        public string Order { get; set; }

        /// <summary>
        /// Choose which fields you want to retrieve values for. (comma-delimited list)
        /// </summary>
        /// <value>The fields to retrieve values for.</value>
        /// <example>id,title,slug</example>
        public string Fields { get; set; }

        /// <summary>
        /// Use Ghost Query Language to build complex query.
        /// (warning: their implementation seems buggy, returning "Error parsing filter" when combining rules)
        /// </summary>
        /// <value>A query using the Ghost Query Language.</value>
        /// <example>
        /// Get pages instead of posts => page:true
        /// Combine rules (AND) => filter=status:published+page:false
        /// Combine rules (OR)  => filter=featured:true,feature_image:-null
        /// </example>
        /// <remarks>
        /// Ghost Query Language: https://github.com/TryGhost/GQL
        /// Filters with multiple rules: https://api.ghost.org/docs/filter#filters-with-multiple-rules
        /// </remarks>
        public string Filter { get; set; }

        /// <summary>
        /// id or slug changes query to a read request.
        /// </summary>
        /// <value>The resource.</value>
        public string Resource { get; set; }

        /// <summary>
        /// Choose which post formats to return. (default is HTML, comma-delimited list)
        /// </summary>
        /// <value>The post formats to return. (i.e. html, mobiledoc, plaintext</value>
        /// <example>html,mobiledoc,plaintext</example>
        public string Formats { get; set; }
    }
}
