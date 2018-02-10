using System.Collections.Generic;
using GhostSharp.Entities;
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

            return Shared.Execute<AuthToken>(siteUrl, request);
        }

        public List<Post> GetPosts()
        {
            var request = new RestRequest("posts", Method.GET);
          
            request.AddQueryParameter("limit", "2");
          
            AppendSecurity(request);

            return Shared.Execute<PostResult>(siteUrl, request).Posts;
        }

        public List<Post> GetPages()
        {
            var request = new RestRequest("posts", Method.GET);

            request.AddQueryParameter("filter", "page:true");
          
            AppendSecurity(request);

            return Shared.Execute<PostResult>(siteUrl, request).Posts;
        }

        public bool IsPublicApiEnabled(string clientId = null, string clientSecret = null)
        {
            var id = clientId ?? this.clientId;
            var secret = clientSecret ?? this.clientSecret;

            // todo: make some really tiny call, just to verify the public API is active.. assuming there isn't an API call just for this

            return false;
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
