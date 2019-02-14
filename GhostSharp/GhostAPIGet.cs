using System;
using System.Linq;
using GhostSharp.Entities;
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

            if (queryParams != null)
            {
                ApplyPostQueryParams(request, queryParams);

                if (queryParams.IncludeTags)
                {
                    request.AddQueryParameter("include", "author");
                    var authorResponse = Execute<PostResponse<PostWithAuthor>>(request);
                    return StandardizePostResponseWithAuthor(authorResponse);
                }
            }

            var plainResponse = Execute<PostResponse<PostWithoutAuthor>>(request);
            return plainResponse == null ? null : StandardizePostResponseWithoutAuthor(plainResponse);
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
           
            return GetSinglePost(request, queryParams);
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

            return GetSinglePost(request, queryParams);
        }

        /// <summary>
        /// Get a collection of tags,
        /// including meta data about pagination so you can retrieve data in chunks.
        /// </summary>
        /// <returns>The tags.</returns>
        /// <param name="queryParams">Parameters that affect which tags are returned.</param>
        public TagResponse GetTags(TagQueryParams queryParams = null)
        {
            var request = new RestRequest("tags", Method.GET);

            request.AddQueryParameter("key", key);

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
            var request = new RestRequest($"tags/{id}", Method.GET);

            request.AddQueryParameter("key", key);

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
            var request = new RestRequest($"tags/slug/{slug}", Method.GET);

            request.AddQueryParameter("key", key);

            ApplyTagQueryParams(request, queryParams);

            return Execute<TagResponse>(request)?.Tags?.Single();
        }

        /// <summary>
        /// Get a collection of active authors,
        /// including meta data about pagination so you can retrieve data in chunks.
        /// </summary>
        /// <returns>The authors.</returns>
        /// <param name="queryParams">Parameters that affect which authors are returned.</param>
        public AuthorResponse GetAuthors(AuthorQueryParams queryParams = null)
        {
            var request = new RestRequest("authors", Method.GET);

            request.AddQueryParameter("key", key);

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
            var request = new RestRequest($"authors/{id}", Method.GET);

            request.AddQueryParameter("key", key);

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
            var request = new RestRequest($"authors/slug/{slug}", Method.GET);

            request.AddQueryParameter("key", key);

            ApplyAuthorQueryParams(request, queryParams);

            return Execute<AuthorResponse>(request)?.Authors?.Single();
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
                if (queryParams.IncludeTags)
                    request.AddQueryParameter("include", "tags");

                if (queryParams.Limit > 0)
                    request.AddQueryParameter("limit", queryParams.Limit.ToString());

                if (queryParams.Page > 0)
                    request.AddQueryParameter("page", queryParams.Page.ToString());

                if (!String.IsNullOrEmpty(queryParams.Order))
                    request.AddQueryParameter("order", queryParams.Order);

                if (!String.IsNullOrEmpty(queryParams.Fields))
                    request.AddQueryParameter("fields", queryParams.Fields);

                if (!String.IsNullOrEmpty(queryParams.Filter))
                    request.AddQueryParameter("filter", queryParams.Filter);

                if (!String.IsNullOrEmpty(queryParams.Resource))
                    request.AddQueryParameter("resource", queryParams.Resource);

                if (!String.IsNullOrEmpty(queryParams.Formats))
                    request.AddQueryParameter("formats", queryParams.Formats);

                if (!String.IsNullOrEmpty(queryParams.Status))
                    request.AddQueryParameter("status", queryParams.Status);
            }
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
                if (queryParams.Limit > 0)
                    request.AddQueryParameter("limit", queryParams.Limit.ToString());

                if (queryParams.Page > 0)
                    request.AddQueryParameter("page", queryParams.Page.ToString());

                if (!String.IsNullOrEmpty(queryParams.Order))
                    request.AddQueryParameter("order", queryParams.Order);

                if (!String.IsNullOrEmpty(queryParams.Include))
                    request.AddQueryParameter("include", queryParams.Include);

                if (!String.IsNullOrEmpty(queryParams.Fields))
                    request.AddQueryParameter("fields", queryParams.Fields);

                if (!String.IsNullOrEmpty(queryParams.Filter))
                    request.AddQueryParameter("filter", queryParams.Filter);

                if (!String.IsNullOrEmpty(queryParams.Resource))
                    request.AddQueryParameter("resource", queryParams.Resource);
            }
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
                if (queryParams.Limit > 0)
                    request.AddQueryParameter("limit", queryParams.Limit.ToString());

                if (queryParams.Page > 0)
                    request.AddQueryParameter("page", queryParams.Page.ToString());

                if (!String.IsNullOrEmpty(queryParams.Order))
                    request.AddQueryParameter("order", queryParams.Order);

                if (!String.IsNullOrEmpty(queryParams.Include))
                    request.AddQueryParameter("include", queryParams.Include);

                if (!String.IsNullOrEmpty(queryParams.Fields))
                    request.AddQueryParameter("fields", queryParams.Fields);

                if (!String.IsNullOrEmpty(queryParams.Filter))
                    request.AddQueryParameter("filter", queryParams.Filter);
            }
        }

        /// <summary>
        /// Get a single post, given a request and query parameters
        /// </summary>
        /// <returns>A single post.</returns>
        /// <param name="request">A resource request.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        Post GetSinglePost(RestRequest request, PostQueryParams queryParams)
        {
            request.AddQueryParameter("key", key);

            ApplyPostQueryParams(request, queryParams);

            if (queryParams != null && queryParams.IncludeTags)
            {
                request.AddQueryParameter("include", "author");
                var postWithAuthor = Execute<PostResponse<PostWithAuthor>>(request).Posts?.Single();
                return postWithAuthor == null ? null : StandardizePostWithAuthor(postWithAuthor);
            }

            var postWithoutAuthor = Execute<PostResponse<PostWithoutAuthor>>(request)?.Posts?.Single();
            return postWithoutAuthor == null ? null : StandardizePostWithoutAuthor(postWithoutAuthor);
        }
    }
}
