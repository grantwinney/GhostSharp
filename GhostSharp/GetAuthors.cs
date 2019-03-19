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
        /// Get a collection of active authors,
        /// including meta data about pagination so you can retrieve data in chunks.
        /// </summary>
        /// <returns>The authors.</returns>
        /// <param name="queryParams">Parameters that affect which authors are returned.</param>
        public AuthorResponse GetAuthors(AuthorQueryParams queryParams = null)
        {
            var request = new RestRequest("authors/", Method.GET);
            ApplyAuthorQueryParams(request, queryParams);
            return Execute<AuthorResponse>(request);
        }

        /// <summary>
        /// Get a specific author based on their ID.
        /// </summary>
        /// <returns>The author matching the given ID.</returns>
        /// <param name="id">The ID of the author to retrieve.</param>
        /// <param name="queryParams">Query parameters.</param>
        public Author GetAuthorById(string id, AuthorQueryParams queryParams = null)
        {
            var request = new RestRequest($"authors/{id}/", Method.GET);
            ApplyAuthorQueryParams(request, queryParams);
            return Execute<AuthorResponse>(request)?.Authors?.Single();
        }

        /// <summary>
        /// Get a specific author based on their slug.
        /// </summary>
        /// <returns>The author matching the given slug.</returns>
        /// <param name="slug">The slug of the author to retrieve.</param>
        /// <param name="queryParams">Query parameters.</param>
        public Author GetAuthorBySlug(string slug, AuthorQueryParams queryParams = null)
        {
            var request = new RestRequest($"authors/slug/{slug}/", Method.GET);
            ApplyAuthorQueryParams(request, queryParams);
            return Execute<AuthorResponse>(request)?.Authors?.Single();
        }

        /// <summary>
        /// Applies any specified parameters to the user request.
        /// </summary>
        /// <param name="request">A user REST request.</param>
        /// <param name="queryParams">Query parameters.</param>
        void ApplyAuthorQueryParams(RestRequest request, AuthorQueryParams queryParams)
        {
            if (queryParams != null)
            {
                if (queryParams.IncludePostCount)
                    request.AddQueryParameter("include", "count.posts");

                if (queryParams.Fields != 0)
                    request.AddQueryParameter("fields", Ext.GetQueryStringFromFlagsEnum<AuthorFields>(queryParams.Fields));

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

