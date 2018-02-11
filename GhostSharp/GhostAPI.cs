using System;
using System.Collections.Generic;
using System.Linq;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using RestSharp;

namespace GhostSharp
{
    /// <summary>
    /// Provides methods for authenticating with the Ghost API,
    /// as well as accessing its various endpoints.
    /// </summary>
    public class GhostAPI
    {
        string siteUrl;

        readonly string clientId;
        readonly string clientSecret;

        public string AuthorizationToken { get; }
        readonly bool isAuthorized;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostAPI"/> class.
        /// </summary>
        /// <param name="siteUrl">The site URL for which to access the API.</param>
        /// <param name="clientId">Client identifier.</param>
        /// <param name="clientSecret">Client secret.</param>
        public GhostAPI(string siteUrl, string clientId, string clientSecret)
        {
            this.siteUrl = siteUrl;
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostAPI"/> class.
        /// </summary>
        /// <param name="siteUrl">The site URL for which to access the API.</param>
        /// <param name="clientId">Client identifier.</param>
        /// <param name="clientSecret">Client secret.</param>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        public GhostAPI(string siteUrl, string clientId, string clientSecret, string username, string password)
        {
            this.siteUrl = siteUrl;
            this.clientId = clientId;
            this.clientSecret = clientSecret;

            var token = GetAuthToken(username, password, clientId, clientSecret);
            AuthorizationToken = token.AccessToken;
            isAuthorized = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostAPI"/> class.
        /// </summary>
        /// <param name="siteUrl">The site URL for which to access the API.</param>
        /// <param name="authToken">Authorization token.</param>
        public GhostAPI(string siteUrl, string authToken)
        {
            this.siteUrl = siteUrl;
            this.AuthorizationToken = authToken;
            isAuthorized = true;
        }

        /// <summary>
        /// Get a collection of published posts.
        /// </summary>
        /// <returns>The posts.</returns>
        /// <param name="queryParams">Parameters that affect which posts are returned.</param>
        public List<Post> GetPosts(PostQueryParams queryParams = null)
        {
            var request = new RestRequest("posts", Method.GET);
            request.AddQueryParameter("include", "author");
          
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
            }
          
            AppendSecurity(request);

            return Base.Execute<PostResult>(siteUrl, request).Posts;
        }

        /// <summary>
        /// Get a specific post based on its ID.
        /// </summary>
        /// <returns>The post matching the given ID.</returns>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <param name="includeTags">True if tags should be included; otherwise False.</param>
        public Post GetPostById(string id, bool includeTags = false)
        {
            var request = new RestRequest($"posts/{id}", Method.GET);
            request.AddQueryParameter("include", "author");
         
            if (includeTags)
                request.AddQueryParameter("include", "tags");

            AppendSecurity(request);

            return Base.Execute<PostResult>(siteUrl, request).Posts[0];
        }

        /// <summary>
        /// Get a specific post based on its slug.
        /// </summary>
        /// <returns>The post matching the given slug.</returns>
        /// <param name="slug">The slug of the post to retrieve.</param>
        /// <param name="includeTags">True if tags should be included; otherwise False.</param>
        public Post GetPostBySlug(string slug, bool includeTags = false)
        {
            var request = new RestRequest($"posts/slug/{slug}", Method.GET);
            request.AddQueryParameter("include", "author");

            if (includeTags)
                request.AddQueryParameter("include", "tags");

            AppendSecurity(request);

            return Base.Execute<PostResult>(siteUrl, request).Posts.First();
        }

        /// <summary>
        /// Get a collection of tags.
        /// </summary>
        /// <returns>The tags.</returns>
        /// <param name="queryParams">Parameters that affect which tags are returned.</param>
        public List<Tag> GetTags(TagQueryParams queryParams = null)
        {
            var request = new RestRequest("tags", Method.GET);

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

            AppendSecurity(request);

            return Base.Execute<List<Tag>>(siteUrl, request);
        }

        /// <summary>
        /// Get a specific tag based on its ID.
        /// </summary>
        /// <returns>The tag matching the given ID.</returns>
        /// <param name="id">The ID of the tag to retrieve.</param>
        /// <param name="include">count.posts (I have no idea what this is for; not documented)</param>
        public Tag GetTagById(string id, string include = null)
        {
            var request = new RestRequest($"tags/{id}", Method.GET);

            if (include != null)
                request.AddQueryParameter("include", include);

            AppendSecurity(request);

            return Base.Execute<List<Tag>>(siteUrl, request).First();
        }

        /// <summary>
        /// Get a specific tag based on its slug.
        /// </summary>
        /// <returns>The tag matching the given slug.</returns>
        /// <param name="slug">The slug of the tag to retrieve.</param>
        /// <param name="include">count.posts (I have no idea what this is for; not documented)</param>
        public Tag GetTagBySlug(string slug, string include = null)
        {
            var request = new RestRequest($"tags/slug/{slug}", Method.GET);

            if (include != null)
                request.AddQueryParameter("include", include);

            AppendSecurity(request);

            return Base.Execute<List<Tag>>(siteUrl, request).First();
        }

        /// <summary>
        /// Get a collection of active users.
        /// </summary>
        /// <returns>The users.</returns>
        /// <param name="queryParams">Parameters that affect which users are returned.</param>
        public List<User> GetUsers(UserQueryParams queryParams = null)
        {
            var request = new RestRequest("users", Method.GET);

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

            AppendSecurity(request);

            return Base.Execute<List<User>>(siteUrl, request);
        }

        /// <summary>
        /// Get a specific user based on their ID.
        /// </summary>
        /// <returns>The user matching the given ID.</returns>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <param name="include">count.posts (I have no idea what this is for; not documented)</param>
        public User GetUserById(string id, string include = null)
        {
            var request = new RestRequest($"users/{id}", Method.GET);

            if (include != null)
                request.AddQueryParameter("include", include);

            AppendSecurity(request);

            return Base.Execute<List<User>>(siteUrl, request).First();
        }

        /// <summary>
        /// Get a specific user based on their slug.
        /// </summary>
        /// <returns>The user matching the given slug.</returns>
        /// <param name="slug">The slug of the user to retrieve.</param>
        /// <param name="include">count.posts (I have no idea what this is for; not documented)</param>
        public User GetUserBySlug(string slug, string include = null)
        {
            var request = new RestRequest($"users/slug/{slug}", Method.GET);

            if (include != null)
                request.AddQueryParameter("include", include);

            AppendSecurity(request);

            return Base.Execute<List<User>>(siteUrl, request).First();
        }

        /// <summary>
        /// Determines whether or not the public API is enabled.
        /// </summary>
        /// <returns><c>true</c>, if the public API is enabled, <c>false</c> otherwise.</returns>
        /// <param name="clientId">Client identifier.</param>
        /// <param name="clientSecret">Client secret.</param>
        public bool IsPublicApiEnabled(string clientId = null, string clientSecret = null)
        {
            var id = clientId ?? this.clientId;
            var secret = clientSecret ?? this.clientSecret;

            try
            {
                GetUsers(new UserQueryParams { Limit = 1 });
                return true;
            }
            catch (GhostSharpException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the authorization token.
        /// </summary>
        /// <returns>The authorization token.</returns>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <param name="clientId">Client identifier.</param>
        /// <param name="clientSecret">Client secret.</param>
        public AuthToken GetAuthToken(string username, string password, string clientId, string clientSecret)
        {
            var request = new RestRequest("authentication/token", Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");

            request.AddParameter("grant_type", "password");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);

            return Base.Execute<AuthToken>(siteUrl, request);
        }

        /// <summary>
        /// If there's an auth token available, attach it to the request.
        /// Otherwise, attach the client id and secret.
        /// </summary>
        /// <param name="request">The request being made to the API.</param>
        void AppendSecurity(RestRequest request)
        {
            if (isAuthorized)
            {
                request.AddParameter("Authorization", $"Bearer {AuthorizationToken}", ParameterType.HttpHeader);
            }
            else
            {
                request.AddQueryParameter("client_id", clientId);
                request.AddQueryParameter("client_secret", clientSecret);
            }
        }
    }
}
