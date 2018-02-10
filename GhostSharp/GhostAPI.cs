using System;
using System.Collections.Generic;
using System.Linq;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using RestSharp;

namespace GhostSharp
{
    public class GhostAPI
    {
        string siteUrl;

        readonly string clientId;
        readonly string clientSecret;

        public string AuthorizationToken { get; }
        readonly bool isAuthorized;

        public GhostAPI(string siteUrl, string clientId, string clientSecret)
        {
            this.siteUrl = siteUrl;
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        public GhostAPI(string siteUrl, string clientId, string clientSecret, string username, string password)
        {
            this.siteUrl = siteUrl;
            this.clientId = clientId;
            this.clientSecret = clientSecret;

            var token = GetAuthToken(username, password, clientId, clientSecret);
            AuthorizationToken = token.AccessToken;
            isAuthorized = true;
        }

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
        /// <param name="postId">The ID of the post to retrieve.</param>
        /// <param name="includeTags">True if tags should be included; otherwise False.</param>
        public Post GetPost(string postId, bool includeTags = false)
        {
            var request = new RestRequest($"posts/{postId}", Method.GET);
            request.AddQueryParameter("include", "author");

            if (includeTags)
                request.AddQueryParameter("include", "tags");

            AppendSecurity(request);

            return Base.Execute<PostResult>(siteUrl, request).Posts.First();
        }

        public bool IsPublicApiEnabled(string clientId = null, string clientSecret = null)
        {
            var id = clientId ?? this.clientId;
            var secret = clientSecret ?? this.clientSecret;

            // todo: make some really tiny call, just to verify the public API is active.. assuming there isn't an API call just for this

            return false;
        }

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
