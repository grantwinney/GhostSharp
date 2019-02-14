using System;
using System.Linq;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using Newtonsoft.Json;
using RestSharp;

namespace GhostSharp
{
    /// <summary>
    /// Processing successful and error responses from the Ghost API.
    /// </summary>
    public sealed partial class GhostAPI
    {
        Uri host;
        const string PATH_AND_VERSION = "/ghost/api/v2/content/";

        readonly string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:GhostSharp.GhostAPI"/> class.
        /// </summary>
        /// <param name="host">The Host for which to access the API.</param>
        /// <param name="key">Content API key.</param>
        public GhostAPI(string host, string key)
        {
            this.key = key;
            this.host = new Uri(new Uri(host), PATH_AND_VERSION);
            ExceptionLevel = ExceptionLevel.All;
        }

        /// <summary>
        /// Specify which exceptions to rethrow, if any. Default is All.
        /// </summary>
        public ExceptionLevel ExceptionLevel { private get; set; }

        /// <summary>
        /// Gets the last exception that was thrown.
        /// </summary>
        /// <value>The last exception.</value>
        public Exception LastException { get; private set; }

        /// <summary>
        /// Calls the Ghost API and returns the response data.
        /// If exceptions are suppressed, returns null on failure.
        /// </summary>
        /// <returns>The API response.</returns>
        /// <param name="request">A RestRequest representing the resource being requested.</param>
        /// <typeparam name="T">The type of object being requested</typeparam>
        T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient { BaseUrl = host };

            try
            {
                var response = client.Execute<T>(request);

                TestResponseForErrors(response);
                TestResponseForException(response, request);

                return response.Data;
            }
            catch (GhostSharpException)
            {
                if (ExceptionLevel == ExceptionLevel.Ghost || ExceptionLevel == ExceptionLevel.All)
                    throw;

                return default(T);
            }
            catch
            {
                if (ExceptionLevel == ExceptionLevel.NonGhost || ExceptionLevel == ExceptionLevel.All)
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
