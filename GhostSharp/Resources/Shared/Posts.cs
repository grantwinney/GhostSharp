using System.Linq;
using GhostSharp.Entities;
using GhostSharp.Enums;
using GhostSharp.QueryParams;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAPI
    {
        public PostResponse GetPosts(PostQueryParams queryParams = null)
        {
            var request = new RestRequest("posts/", Method.GET);
            ApplyPostQueryParams(request, queryParams);
            return Execute<PostResponse>(request);
        }

        public Post GetPostById(string id, PostQueryParams queryParams = null)
        {
            var request = new RestRequest($"posts/{id}/", Method.GET);
            ApplyPostQueryParams(request, queryParams);
            return Execute<PostResponse>(request)?.Posts?.Single();
        }

        public Post GetPostBySlug(string slug, PostQueryParams queryParams = null)
        {
            var request = new RestRequest($"posts/slug/{slug}/", Method.GET);
            ApplyPostQueryParams(request, queryParams);
            return Execute<PostResponse>(request)?.Posts?.Single();
        }

        /// <summary>
        /// Applies any specified parameters to the post request.
        /// </summary>
        /// <param name="request">A post REST request.</param>
        /// <param name="queryParams">Query parameters.</param>
        private void ApplyPostQueryParams(RestRequest request, PostQueryParams queryParams)
        {
            if (queryParams != null)
            {
                if (queryParams.IncludeAuthors && queryParams.IncludeTags)
                    request.AddQueryParameter("include", "authors,tags");
                else if (queryParams.IncludeAuthors)
                    request.AddQueryParameter("include", "authors");
                else if (queryParams.IncludeTags)
                    request.AddQueryParameter("include", "tags");

                if (queryParams.Fields != 0)
                    request.AddQueryParameter("fields", Ext.GetQueryStringFromFlagsEnum<PostFields>(queryParams.Fields));

                if (queryParams.Fields2 != 0)
                    request.AddQueryParameter("fields", Ext.GetQueryStringFromFlagsEnum<PostFields2>(queryParams.Fields));

                if (!string.IsNullOrWhiteSpace(queryParams.Filter))
                    request.AddQueryParameter("filter", queryParams.Filter);

                if (queryParams.Formats != 0)
                    request.AddQueryParameter("formats", Ext.GetQueryStringFromFlagsEnum<PostFormat>(queryParams.Formats));

                if (queryParams.NoLimit)
                    request.AddQueryParameter("limit", "all");
                else if (queryParams.Limit > 0)
                    request.AddQueryParameter("limit", queryParams.Limit.ToString());

                if (queryParams.Page > 0)
                    request.AddQueryParameter("page", queryParams.Page.ToString());

                if (queryParams.Order?.Any() == true)
                    request.AddQueryParameter("order", Ext.GetOrderQueryString(queryParams.Order));
            }
        }
    }
}
