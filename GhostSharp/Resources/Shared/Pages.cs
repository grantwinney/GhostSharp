using System.Linq;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAPI
    {
        public PageResponse GetPages(PostQueryParams queryParams = null)
        {
            var request = new RestRequest("pages/", Method.GET);
            ApplyPageQueryParams(request, queryParams);
            return Execute<PageResponse>(request);
        }

        public Post GetPageById(string id, PostQueryParams queryParams = null)
        {
            var request = new RestRequest($"pages/{id}/", Method.GET);
            ApplyPageQueryParams(request, queryParams);
            return Execute<PageResponse>(request)?.Pages?.Single();
        }

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
