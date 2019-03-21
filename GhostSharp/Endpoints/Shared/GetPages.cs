using System.Linq;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAPI
    {
        /// <summary>
        /// Get a collection of published pages,
        /// including meta data about pagination so you can retrieve data in chunks.
        /// </summary>
        /// <returns>The pages.</returns>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public PageResponse GetPages(PostQueryParams queryParams = null)
        {
            var request = new RestRequest("pages/", Method.GET);
            ApplyPageQueryParams(request, queryParams);
            return Execute<PageResponse>(request);
        }

        /// <summary>
        /// Get a specific page based on its ID.
        /// </summary>
        /// <returns>The page matching the given ID.</returns>
        /// <param name="id">The ID of the page to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public Page GetPageById(string id, PostQueryParams queryParams = null)
        {
            var request = new RestRequest($"pages/{id}/", Method.GET);
            ApplyPageQueryParams(request, queryParams);
            return Execute<PageResponse>(request)?.Pages?.Single();
        }

        /// <summary>
        /// Get a specific page based on its slug.
        /// </summary>
        /// <returns>The page matching the given slug.</returns>
        /// <param name="slug">The slug of the page to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public Post GetPageBySlug(string slug, PostQueryParams queryParams = null)
        {
            var request = new RestRequest($"pages/slug/{slug}/", Method.GET);
            ApplyPageQueryParams(request, queryParams);
            return Execute<PageResponse>(request)?.Pages?.Single();
        }

        /// <summary>
        /// Applies any specified parameters to the page request.
        /// </summary>
        /// <param name="request">A page REST request.</param>
        /// <param name="queryParams">Query parameters.</param>
        void ApplyPageQueryParams(RestRequest request, PostQueryParams queryParams)
        {
            ApplyPostQueryParams(request, queryParams);
        }
    }
}

