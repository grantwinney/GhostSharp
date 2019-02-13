using System;
using System.Linq;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using Newtonsoft.Json;
using RestSharp;

namespace GhostSharp
{
    /// <summary>
    /// Provides methods for authenticating with the Ghost API,
    /// as well as accessing its various endpoints.
    /// </summary>
    public sealed partial class GhostAPI
    {
        Uri baseUri;
        const string BASE_REQUEST_URI = "/ghost/api/v0.1";

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
            : this(siteUrl)
        {
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
            : this(siteUrl)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;

            var token = GetAuthToken(clientId, clientSecret, username, password);
            AuthorizationToken = token.AccessToken;
            isAuthorized = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostAPI"/> class.
        /// </summary>
        /// <param name="siteUrl">The site URL for which to access the API.</param>
        /// <param name="authToken">Authorization token.</param>
        public GhostAPI(string siteUrl, string authToken)
            : this(siteUrl)
        {
            AuthorizationToken = authToken;
            isAuthorized = true;
        }

        GhostAPI(string siteUrl)
        {
            baseUri = new Uri(new Uri(siteUrl), BASE_REQUEST_URI);
        }

        /// <summary>
        /// Specify which exceptions to suppress, if any. Default is None.
        /// </summary>
        public SuppressionLevel SuppressionLevel { private get; set; }

        /// <summary>
        /// Gets the last exception that was thrown.
        /// </summary>
        /// <value>The last exception.</value>
        public Exception LastException { get; private set; }

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
                return GetUsers(new UserQueryParams { Limit = 1, Fields = "id" }).Users != null;
            }
            catch (GhostSharpException)
            {
                if (SuppressionLevel == SuppressionLevel.None)
                    throw;
                return false;
            }
            catch (Exception)
            {
                if (SuppressionLevel == SuppressionLevel.All)
                    return false;
                throw;
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
        public AuthToken GetAuthToken(string clientId, string clientSecret, string username, string password)
        {
            var request = new RestRequest("authentication/token", Method.POST);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");

            request.AddParameter("grant_type", "password");
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            return Execute<AuthToken>(request);
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

        /// <summary>
        /// Calls the Ghost API and returns a value indicating whether or not it succeeded.
        /// If exceptions are suppressed, returns False on failure.
        /// </summary>
        /// <returns>The API success status.</returns>
        /// <param name="request">A RestRequest representing the resource being requested.</param>
        /// <param name="suppressException">False to throw any exceptions; True to suppress them.</param>
        bool Execute(RestRequest request, bool suppressException = false)
        {
            var client = new RestClient { BaseUrl = baseUri };

            try
            {
                var response = client.Execute(request);

                TestResponseForErrors(response);
                TestResponseForException(response, request);

                return response.IsSuccessful;
            }
            catch (GhostSharpException)
            {
                if (SuppressionLevel == SuppressionLevel.None)
                    throw;

                return false;
            }
            catch
            {
                if (SuppressionLevel != SuppressionLevel.All)
                    throw;

                return false;
            }
        }

        /// <summary>
        /// Calls the Ghost API and returns the response data.
        /// If exceptions are suppressed, returns null on failure.
        /// </summary>
        /// <returns>The API response.</returns>
        /// <param name="request">A RestRequest representing the resource being requested.</param>
        /// <typeparam name="T">The type of object being requested</typeparam>
        T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient { BaseUrl = baseUri };

            try
            {
                var response = client.Execute<T>(request);

                TestResponseForErrors(response);
                TestResponseForException(response, request);

                return response.Data;
            }
            catch (GhostSharpException)
            {
                if (SuppressionLevel == SuppressionLevel.None)
                    throw;

                return default(T);
            }
            catch
            {
                if (SuppressionLevel != SuppressionLevel.All)
                    throw;

                return default(T);
            }
        }

        /// <summary>
        /// If the response content has one or more error messages, throw an exception.
        /// </summary>
        /// <param name="response">The API response</param>
        void TestResponseForErrors(IRestResponse response)
        {
            var apiFailure = JsonConvert.DeserializeObject<GhostApiFailure>(response.Content);
            if (apiFailure != null && apiFailure.Errors != null)
            {
                var ex = new GhostSharpException(apiFailure.Errors);
                LastException = ex;

                if (SuppressionLevel == SuppressionLevel.None)
                    throw ex;
            }
        }

        /// <summary>
        /// If the response returns an exception, add a message and throw it.
        /// </summary>
        /// <param name="response">The API response</param>
        /// <param name="request">The original request to the API.</param>
        void TestResponseForException(IRestResponse response, RestRequest request)
        {
            if (response.ErrorException != null)
            {
                var ex = new GhostSharpException($"Unable to {request.Method} /{request.Resource}: {response.ResponseStatus}", response.ErrorException);
                LastException = ex;
              
                if (SuppressionLevel == SuppressionLevel.None)
                    throw ex;
            }
        }

        /// <summary>
        /// If a request is made that omits author metadata in the response,
        /// retain the author id from the response, but leave author null.
        /// </summary>
        /// <returns>A standardized Response instance</returns>
        /// <param name="response">The response with only an author id in the post.</param>
        static PostResponse StandardizePostResponseWithoutAuthor(PostResponse<PostWithoutAuthor> response)
        {
            return new PostResponse
            {
                Posts = response.Posts.Select(StandardizePostWithoutAuthor).ToList(),
                Meta = response.Meta
            };
        }

        /// <summary>
        /// If a request is made to include author metadata in the response,
        /// make sure the author id field is filled in using that metadata.
        /// </summary>
        /// <returns>A standardized Response instance</returns>
        /// <param name="response">The response with full author metadata in the post.</param>
        static PostResponse StandardizePostResponseWithAuthor(PostResponse<PostWithAuthor> response)
        {
            return new PostResponse
            {
                Posts = response.Posts.Select(StandardizePostWithAuthor).ToList(),
                Meta = response.Meta
            };
        }

        /// <summary>
        /// If a request is made that omits author metadata in the response,
        /// retain the author id from the response, but leave author null.
        /// </summary>
        /// <returns>A standardized Post instance</returns>
        /// <param name="post">The post with only an author id.</param>
        static Post StandardizePostWithoutAuthor(PostWithoutAuthor post)
        {
            return new Post
            {
                Id = post.Id,
                Uuid = post.Uuid,
                Title = post.Title,
                Slug = post.Slug,
                MobileDoc = post.MobileDoc,
                Html = post.Html,
                PlainText = post.PlainText,
                FeatureImage = post.FeatureImage,
                Featured = post.Featured,
                Page = post.Page,
                Status = post.Status,
                Locale = post.Locale,
                Visibility = post.Visibility,
                MetaTitle = post.MetaTitle,
                MetaDescription = post.MetaDescription,
                CreatedAt = post.CreatedAt,
                CreatedBy = post.CreatedBy,
                UpdatedAt = post.UpdatedAt,
                UpdatedBy = post.UpdatedBy,
                PublishedAt = post.PublishedAt,
                PublishedBy = post.PublishedBy,
                CustomExcerpt = post.CustomExcerpt,
                CodeInjectionHead = post.CodeInjectionHead,
                CodeInjectionFoot = post.CodeInjectionFoot,
                OgImage = post.OgImage,
                OgTitle = post.OgTitle,
                OgDescription = post.OgDescription,
                TwitterImage = post.TwitterImage,
                TwitterTitle = post.TwitterTitle,
                TwitterDescription = post.TwitterDescription,
                CustomTemplate = post.CustomTemplate,
                Tags = post.Tags,
                PrimaryTag = post.PrimaryTag,
                Author = null,
                AuthorId = post.Author,
                Url = post.Url,
                CommentId = post.CommentId
            };
        }

        /// <summary>
        /// If a request is made to include author metadata in the response,
        /// make sure the author id field is filled in using that metadata.
        /// </summary>
        /// <returns>A standardized Post instance</returns>
        /// <param name="post">The post with full author metadata.</param>
        static Post StandardizePostWithAuthor(PostWithAuthor post)
        {
            return new Post
            {
                Id = post.Id,
                Uuid = post.Uuid,
                Title = post.Title,
                Slug = post.Slug,
                MobileDoc = post.MobileDoc,
                Html = post.Html,
                PlainText = post.PlainText,
                FeatureImage = post.FeatureImage,
                Featured = post.Featured,
                Page = post.Page,
                Status = post.Status,
                Locale = post.Locale,
                Visibility = post.Visibility,
                MetaTitle = post.MetaTitle,
                MetaDescription = post.MetaDescription,
                CreatedAt = post.CreatedAt,
                CreatedBy = post.CreatedBy,
                UpdatedAt = post.UpdatedAt,
                UpdatedBy = post.UpdatedBy,
                PublishedAt = post.PublishedAt,
                PublishedBy = post.PublishedBy,
                CustomExcerpt = post.CustomExcerpt,
                CodeInjectionHead = post.CodeInjectionHead,
                CodeInjectionFoot = post.CodeInjectionFoot,
                OgImage = post.OgImage,
                OgTitle = post.OgTitle,
                OgDescription = post.OgDescription,
                TwitterImage = post.TwitterImage,
                TwitterTitle = post.TwitterTitle,
                TwitterDescription = post.TwitterDescription,
                CustomTemplate = post.CustomTemplate,
                Tags = post.Tags,
                PrimaryTag = post.PrimaryTag,
                Author = post.Author,
                AuthorId = post.Author.Id,
                Url = post.Url,
                CommentId = post.CommentId
            };
        }

        /// <summary>
        /// Convert a Post to a PostWithoutAuthor before pushing it to Ghost.
        /// </summary>
        /// <returns>A PostWithoutAuthor instance</returns>
        /// <param name="post">The Post to push to Ghost.</param>
        static PostWithoutAuthor ConvertToPostWithoutAuthor(Post post)
        {
            return new PostWithoutAuthor
            {
                Id = post.Id,
                Uuid = post.Uuid,
                Title = post.Title,
                Slug = post.Slug,
                MobileDoc = post.MobileDoc,
                Html = post.Html,
                PlainText = post.PlainText,
                FeatureImage = post.FeatureImage,
                Featured = post.Featured,
                Page = post.Page,
                Status = post.Status,
                Locale = post.Locale,
                Visibility = post.Visibility,
                MetaTitle = post.MetaTitle,
                MetaDescription = post.MetaDescription,
                CreatedAt = post.CreatedAt,
                CreatedBy = post.CreatedBy,
                UpdatedAt = post.UpdatedAt,
                UpdatedBy = post.UpdatedBy,
                PublishedAt = post.PublishedAt,
                PublishedBy = post.PublishedBy,
                CustomExcerpt = post.CustomExcerpt,
                CodeInjectionHead = post.CodeInjectionHead,
                CodeInjectionFoot = post.CodeInjectionFoot,
                OgImage = post.OgImage,
                OgTitle = post.OgTitle,
                OgDescription = post.OgDescription,
                TwitterImage = post.TwitterImage,
                TwitterTitle = post.TwitterTitle,
                TwitterDescription = post.TwitterDescription,
                CustomTemplate = post.CustomTemplate,
                Tags = post.Tags,
                PrimaryTag = post.PrimaryTag,
                Author = post.AuthorId,
                Url = post.Url,
                CommentId = post.CommentId
            };
        }
    }
}
