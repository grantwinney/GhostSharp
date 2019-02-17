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
        /// Get a collection of published pages,
        /// including meta data about pagination so you can retrieve data in chunks.
        /// </summary>
        /// <returns>The pages.</returns>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public PageResponse GetPages(PageQueryParams queryParams = null)
        {
            var request = new RestRequest("pages", Method.GET);
            request.AddQueryParameter("key", key);
            ApplyPageQueryParams(request, queryParams);
            return Execute<PageResponse>(request);
        }

        /// <summary>
        /// Get a specific page based on its ID.
        /// </summary>
        /// <returns>The page matching the given ID.</returns>
        /// <param name="id">The ID of the page to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public Page GetPageById(string id, PageQueryParams queryParams = null)
        {
            var request = new RestRequest($"pages/{id}", Method.GET);
            request.AddQueryParameter("key", key);
            ApplyPageQueryParams(request, queryParams);
            return Execute<PageResponse>(request)?.Pages?.Single();
        }

        /// <summary>
        /// Get a specific page based on its slug.
        /// </summary>
        /// <returns>The page matching the given slug.</returns>
        /// <param name="slug">The slug of the page to retrieve.</param>
        /// <param name="queryParams">Parameters that affect the resultset.</param>
        public Post GetPageBySlug(string slug, PageQueryParams queryParams = null)
        {
            var request = new RestRequest($"pages/slug/{slug}", Method.GET);
            request.AddQueryParameter("key", key);
            ApplyPageQueryParams(request, queryParams);
            return Execute<PageResponse>(request)?.Pages?.Single();
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

        public Settings GetSettings()
        {
            var request = new RestRequest("settings", Method.GET);

            request.AddQueryParameter("key", key);

            return Execute<SettingsResponse>(request)?.Settings;
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

        /// <summary>
        /// Applies any specified parameters to the page request.
        /// </summary>
        /// <param name="request">A page REST request.</param>
        /// <param name="queryParams">Query parameters.</param>
        void ApplyPageQueryParams(RestRequest request, PageQueryParams queryParams)
        {
            ApplyPostQueryParams(request, queryParams);
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

