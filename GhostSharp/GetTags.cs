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
        /// Get a collection of tags,
        /// including meta data about pagination so you can retrieve data in chunks.
        /// </summary>
        /// <returns>The tags.</returns>
        /// <param name="queryParams">Parameters that affect which tags are returned.</param>
        public TagResponse GetTags(TagQueryParams queryParams = null)
        {
            var request = new RestRequest("tags/", Method.GET);
            ApplyTagQueryParams(request, queryParams);
            return Execute<TagResponse>(request);
        }

        /// <summary>
        /// Get a specific tag based on its ID.
        /// </summary>
        /// <returns>The tag matching the given ID.</returns>
        /// <param name="id">The ID of the tag to retrieve.</param>
        /// <param name="include">count.posts (I have no idea what this is for; not documented)</param>
        public Tag GetTagById(string id, TagQueryParams queryParams = null)
        {
            var request = new RestRequest($"tags/{id}/", Method.GET);
            ApplyTagQueryParams(request, queryParams);
            return Execute<TagResponse>(request)?.Tags?.Single();
        }

        /// <summary>
        /// Get a specific tag based on its slug.
        /// </summary>
        /// <returns>The tag matching the given slug.</returns>
        /// <param name="slug">The slug of the tag to retrieve.</param>
        /// <param name="include">count.posts (I have no idea what this is for; not documented)</param>
        public Tag GetTagBySlug(string slug, TagQueryParams queryParams = null)
        {
            var request = new RestRequest($"tags/slug/{slug}/", Method.GET);
            ApplyTagQueryParams(request, queryParams);
            return Execute<TagResponse>(request)?.Tags?.Single();
        }

        /// <summary>
        /// Applies any specified parameters to the tag request.
        /// </summary>
        /// <param name="request">A tag REST request.</param>
        /// <param name="queryParams">Query parameters.</param>
        void ApplyTagQueryParams(RestRequest request, TagQueryParams queryParams)
        {
            if (queryParams != null)
            {
                if (queryParams.IncludePostCount)
                    request.AddQueryParameter("include", "count.posts");

                if (queryParams.Fields != 0)
                    request.AddQueryParameter("fields", Ext.GetQueryStringFromFlagsEnum<TagFields>(queryParams.Fields));

                if (!string.IsNullOrWhiteSpace(queryParams.Filter))
                    request.AddQueryParameter("filter", queryParams.Filter);

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

