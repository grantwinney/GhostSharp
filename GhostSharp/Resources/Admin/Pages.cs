using GhostSharp.Entities;
using GhostSharp.QueryParams;

namespace GhostSharp
{
    public partial class GhostAdminAPI
    {
        /// <summary>
        /// Get a collection of published pages,
        /// including meta data about pagination so you can retrieve data in chunks.
        /// </summary>
        /// <returns>The pages.</returns>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public new PageResponse GetPages(PostQueryParams queryParams = null)
        {
            return base.GetPages(queryParams);
        }

        /// <summary>
        /// Get a specific page based on its ID.
        /// </summary>
        /// <returns>The page matching the given ID. By default, returns MobileDoc format and author and tag data.</returns>
        /// <param name="id">The ID of the page to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public new Page GetPageById(string id, PostQueryParams queryParams = null)
        {
            return base.GetPageById(id, queryParams);
        }

        /// <summary>
        /// Get a specific page based on its slug.
        /// </summary>
        /// <returns>The page matching the given slug. By default, returns MobileDoc format and author and tag data.</returns>
        /// <param name="slug">The slug of the page to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public new Page GetPageBySlug(string slug, PostQueryParams queryParams = null)
        {
            return base.GetPageBySlug(slug, queryParams);
        }
    }
}
