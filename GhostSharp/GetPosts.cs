using System.Linq;
using GhostSharp.Entities;
using GhostSharp.Enums;
using GhostSharp.QueryParams;
using RestSharp;

namespace GhostSharp
{
    public partial class GhostAPI
    {
        /// <summary>
        /// Get a collection of published posts,
        /// including meta data about pagination so you can retrieve data in chunks.
        /// </summary>
        /// <returns>The posts.</returns>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public PostResponse GetPosts(PostQueryParams queryParams = null)
        {
            var request = new RestRequest("posts", Method.GET);
            request.AddQueryParameter("key", key);
            ApplyPostQueryParams(request, queryParams);
            return Execute<PostResponse>(request);
        }

        /// <summary>
        /// Get a specific post based on its ID.
        /// </summary>
        /// <returns>The post matching the given ID.</returns>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public Post GetPostById(string id, PostQueryParams queryParams = null)
        {
            var request = new RestRequest($"posts/{id}", Method.GET);
            request.AddQueryParameter("key", key);
            ApplyPostQueryParams(request, queryParams);
            return Execute<PostResponse>(request)?.Posts?.Single();
        }

        /// <summary>
        /// Get a specific post based on its slug.
        /// </summary>
        /// <returns>The post matching the given slug.</returns>
        /// <param name="slug">The slug of the post to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public Post GetPostBySlug(string slug, PostQueryParams queryParams = null)
        {
            var request = new RestRequest($"posts/slug/{slug}", Method.GET);
            request.AddQueryParameter("key", key);
            ApplyPostQueryParams(request, queryParams);
            return Execute<PostResponse>(request)?.Posts?.Single();
        }

        /// <summary>
        /// Applies any specified parameters to the post request.
        /// </summary>
        /// <param name="request">A post REST request.</param>
        /// <param name="queryParams">Query parameters.</param>
        void ApplyPostQueryParams(RestRequest request, PostQueryParams queryParams)
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

