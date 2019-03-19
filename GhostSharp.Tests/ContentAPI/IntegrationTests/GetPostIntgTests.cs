using GhostSharp.Entities;
using GhostSharp.Enums;
using GhostSharp.QueryParams;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostSharp.Tests.ContentAPI.IntegrationTests
{
    [TestFixture]
    public class GetPostIntgTests : TestBase
    {
        private const int MINIMUM_POST_COUNT_THRESHHOLD = 200;

        private GhostAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostContentAPI(Host, ValidContentApiKey);
        }

        [Test]
        public void GetPostById_ReturnsMatchingPost()
        {
            var post = auth.GetPostById(ValidPost1Id);

            Assert.AreEqual(ValidPost1Id, post.Id);
            Assert.AreEqual(ValidPost1Slug, post.Slug);
            Assert.AreEqual(ValidPost1Title, post.Title);
            Assert.AreEqual(ValidPost1Url, post.Url);

            Assert.IsNotNull(post.Uuid);
            Assert.IsNotNull(post.Html);
            Assert.IsNotNull(post.CommentId);
            Assert.IsNotNull(post.FeatureImage);
            Assert.IsNotNull(post.MetaDescription);
            Assert.IsNotNull(post.CreatedAt);
            Assert.IsNotNull(post.UpdatedAt);
            Assert.IsNotNull(post.PublishedAt);
            Assert.IsNotNull(post.CustomExcerpt);
            Assert.IsNotNull(post.OgDescription);
            Assert.IsNotNull(post.TwitterDescription);
            Assert.IsNotNull(post.Url);
            Assert.IsNotNull(post.Excerpt);

            Assert.IsNull(post.MetaTitle);
            Assert.IsNull(post.CodeInjectionHead);
            Assert.IsNull(post.CodeInjectionFoot);
            Assert.IsNull(post.OgImage);
            Assert.IsNull(post.OgTitle);
            Assert.IsNull(post.TwitterImage);
            Assert.IsNull(post.TwitterTitle);
            Assert.IsNull(post.CustomTemplate);
            Assert.IsNull(post.PrimaryAuthor);
            Assert.IsNull(post.PrimaryTag);
            Assert.IsNull(post.Authors);
            Assert.IsNull(post.Tags);
        }

        [Test]
        public void GetPostById_ReturnsAuthors_WhenIncludingAuthors()
        {
            var post = auth.GetPostById(ValidPost1Id, new PostQueryParams { IncludeAuthors = true });

            Assert.AreEqual(ValidPost1Id, post.Id);
            Assert.AreEqual(ValidPost1Slug, post.Slug);
            Assert.AreEqual(ValidPost1Title, post.Title);
            Assert.AreEqual(ValidPost1Url, post.Url);

            Assert.IsNotNull(post.Authors);
            Assert.IsNotNull(post.PrimaryAuthor);
            Assert.AreEqual(ValidPost1Author, post.Authors[0].Id);
            Assert.AreEqual(ValidPost1Author, post.PrimaryAuthor.Id);

            Assert.IsNull(post.Tags);
            Assert.IsNull(post.PrimaryTag);
        }

        [Test]
        public void GetPostById_ReturnsTags_WhenIncludingTags()
        {
            var post = auth.GetPostById(ValidPost1Id, new PostQueryParams { IncludeTags = true });

            Assert.AreEqual(ValidPost1Id, post.Id);
            Assert.AreEqual(ValidPost1Slug, post.Slug);
            Assert.AreEqual(ValidPost1Title, post.Title);
            Assert.AreEqual(ValidPost1Url, post.Url);

            Assert.IsNotNull(post.Tags);
            Assert.IsNotNull(post.PrimaryTag);
            Assert.Contains(ValidPost1PrimaryTag, post.Tags.Select(x => x.Id).ToList());
            Assert.AreEqual(ValidPost1PrimaryTag, post.PrimaryTag.Id);

            Assert.IsNull(post.Authors);
            Assert.IsNull(post.PrimaryAuthor);
        }

        [Test]
        public void GetPostById_ReturnsAuthorsAndTags_WhenIncludingAuthorsAndTags()
        {
            var post = auth.GetPostById(ValidPost1Id, new PostQueryParams { IncludeAuthors = true, IncludeTags = true });

            Assert.AreEqual(ValidPost1Id, post.Id);
            Assert.AreEqual(ValidPost1Slug, post.Slug);
            Assert.AreEqual(ValidPost1Title, post.Title);
            Assert.AreEqual(ValidPost1Url, post.Url);

            Assert.IsNotNull(post.Tags);
            Assert.IsNotNull(post.PrimaryTag);
            Assert.Contains(ValidPost1PrimaryTag, post.Tags.Select(x => x.Id).ToList());
            Assert.AreEqual(ValidPost1PrimaryTag, post.PrimaryTag.Id);

            Assert.IsNotNull(post.Authors);
            Assert.IsNotNull(post.PrimaryAuthor);
            Assert.AreEqual(ValidPost1Author, post.Authors[0].Id);
            Assert.AreEqual(ValidPost1Author, post.PrimaryAuthor.Id);
        }

        [Test]
        public void GetPostById_ReturnsLimitedFields_WhenFieldsSpecified()
        {
            var post = auth.GetPostById(ValidPost1Id, new PostQueryParams { Fields = PostFields.Id | PostFields.Slug });

            Assert.AreEqual(ValidPost1Id, post.Id);
            Assert.AreEqual(ValidPost1Slug, post.Slug);
            Assert.IsNull(post.Title);
            Assert.IsNull(post.Url);
        }

        [Test]
        public void GetPostBySlug_ReturnsMatchingPost()
        {
            var post = auth.GetPostBySlug(ValidPost1Slug);

            Assert.AreEqual(ValidPost1Id, post.Id);
            Assert.AreEqual(ValidPost1Slug, post.Slug);
            Assert.AreEqual(ValidPost1Title, post.Title);
            Assert.AreEqual(ValidPost1Url, post.Url);

            Assert.IsNotNull(post.Uuid);
            Assert.IsNotNull(post.Html);
            Assert.IsNotNull(post.CommentId);
            Assert.IsNotNull(post.FeatureImage);
            Assert.IsNotNull(post.MetaDescription);
            Assert.IsNotNull(post.CreatedAt);
            Assert.IsNotNull(post.UpdatedAt);
            Assert.IsNotNull(post.PublishedAt);
            Assert.IsNotNull(post.CustomExcerpt);
            Assert.IsNotNull(post.OgDescription);
            Assert.IsNotNull(post.TwitterDescription);
            Assert.IsNotNull(post.Url);
            Assert.IsNotNull(post.Excerpt);

            Assert.IsNull(post.MetaTitle);
            Assert.IsNull(post.CodeInjectionHead);
            Assert.IsNull(post.CodeInjectionFoot);
            Assert.IsNull(post.OgImage);
            Assert.IsNull(post.OgTitle);
            Assert.IsNull(post.TwitterImage);
            Assert.IsNull(post.TwitterTitle);
            Assert.IsNull(post.CustomTemplate);
            Assert.IsNull(post.PrimaryAuthor);
            Assert.IsNull(post.PrimaryTag);
            Assert.IsNull(post.Authors);
            Assert.IsNull(post.Tags);
        }

        [Test]
        public void GetPostBySlug_ReturnsAuthors_WhenIncludingAuthors()
        {
            var post = auth.GetPostBySlug(ValidPost1Slug, new PostQueryParams { IncludeAuthors = true });

            Assert.AreEqual(ValidPost1Id, post.Id);
            Assert.AreEqual(ValidPost1Slug, post.Slug);
            Assert.AreEqual(ValidPost1Title, post.Title);
            Assert.AreEqual(ValidPost1Url, post.Url);

            Assert.IsNotNull(post.Authors);
            Assert.IsNotNull(post.PrimaryAuthor);
            Assert.AreEqual(ValidPost1Author, post.Authors[0].Id);
            Assert.AreEqual(ValidPost1Author, post.PrimaryAuthor.Id);

            Assert.IsNull(post.Tags);
            Assert.IsNull(post.PrimaryTag);
        }

        [Test]
        public void GetPostBySlug_ReturnsTags_WhenIncludingTags()
        {
            var post = auth.GetPostBySlug(ValidPost1Slug, new PostQueryParams { IncludeTags = true });

            Assert.AreEqual(ValidPost1Id, post.Id);
            Assert.AreEqual(ValidPost1Slug, post.Slug);
            Assert.AreEqual(ValidPost1Title, post.Title);
            Assert.AreEqual(ValidPost1Url, post.Url);

            Assert.IsNotNull(post.Tags);
            Assert.IsNotNull(post.PrimaryTag);
            Assert.Contains(ValidPost1PrimaryTag, post.Tags.Select(x => x.Id).ToList());
            Assert.AreEqual(ValidPost1PrimaryTag, post.PrimaryTag.Id);

            Assert.IsNull(post.Authors);
            Assert.IsNull(post.PrimaryAuthor);
        }

        [Test]
        public void GetPostBySlug_ReturnsAuthorsAndTags_WhenIncludingAuthorsAndTags()
        {
            var post = auth.GetPostBySlug(ValidPost1Slug, new PostQueryParams { IncludeAuthors = true, IncludeTags = true });

            Assert.AreEqual(ValidPost1Id, post.Id);
            Assert.AreEqual(ValidPost1Slug, post.Slug);
            Assert.AreEqual(ValidPost1Title, post.Title);
            Assert.AreEqual(ValidPost1Url, post.Url);

            Assert.IsNotNull(post.Tags);
            Assert.IsNotNull(post.PrimaryTag);
            Assert.Contains(ValidPost1PrimaryTag, post.Tags.Select(x => x.Id).ToList());
            Assert.AreEqual(ValidPost1PrimaryTag, post.PrimaryTag.Id);

            Assert.IsNotNull(post.Authors);
            Assert.IsNotNull(post.PrimaryAuthor);
            Assert.AreEqual(ValidPost1Author, post.Authors[0].Id);
            Assert.AreEqual(ValidPost1Author, post.PrimaryAuthor.Id);
        }

        [Test]
        public void GetPostBySlug_ReturnsLimitedFields_WhenFieldsSpecified()
        {
            var post = auth.GetPostBySlug(ValidPost1Slug, new PostQueryParams { Fields = PostFields.Id | PostFields.Slug });

            Assert.AreEqual(ValidPost1Id, post.Id);
            Assert.AreEqual(ValidPost1Slug, post.Slug);
            Assert.IsNull(post.Title);
            Assert.IsNull(post.Url);
        }

        [Test]
        public void GetPosts_ReturnsLimitedPosts_WhenLimitSpecified()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var postResponse = auth.GetPosts(new PostQueryParams { Limit = 1, Fields = PostFields.Id });

            Assert.AreEqual(1, postResponse.Posts.Count);
        }

        [Test]
        public void GetPosts_ReturnsLimitedFields_WhenFieldsSpecified()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var post = auth.GetPosts(new PostQueryParams { Limit = 1, Fields = PostFields.Id }).Posts[0];

            Assert.IsNotNull(post.Id);

            Assert.IsNull(post.Authors);
            Assert.IsNull(post.CodeInjectionFoot);
            Assert.IsNull(post.CodeInjectionHead);
            Assert.IsNull(post.CommentId);
            Assert.IsNull(post.CreatedAt);
            Assert.IsNull(post.CustomExcerpt);
            Assert.IsNull(post.CustomTemplate);
            Assert.IsNull(post.Excerpt);
            Assert.IsNull(post.Featured);
            Assert.IsNull(post.FeatureImage);
            Assert.IsNull(post.Html);
            Assert.IsNull(post.MetaDescription);
            Assert.IsNull(post.MetaTitle);
            Assert.IsNull(post.MobileDoc);
            Assert.IsNull(post.OgDescription);
            Assert.IsNull(post.OgImage);
            Assert.IsNull(post.OgTitle);
            Assert.IsNull(post.Page);
            Assert.IsNull(post.PlainText);
            Assert.IsNull(post.PrimaryAuthor);
            Assert.IsNull(post.PrimaryTag);
            Assert.IsNull(post.PublishedAt);
            Assert.IsNull(post.Slug);
            Assert.IsNull(post.Tags);
            Assert.IsNull(post.Title);
            Assert.IsNull(post.TwitterDescription);
            Assert.IsNull(post.TwitterImage);
            Assert.IsNull(post.TwitterTitle);
            Assert.IsNull(post.UpdatedAt);
            Assert.IsNull(post.Url);
            Assert.IsNull(post.Uuid);
        }

        [Test]
        public void GetAuthors_ReturnsAuthors_WhenIncludingAuthors()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var post = auth.GetPosts(new PostQueryParams { Limit = 1, IncludeAuthors = true }).Posts[0];

            Assert.IsNotNull(post.Authors);
            Assert.IsNotNull(post.PrimaryAuthor);

            Assert.IsNull(post.Tags);
            Assert.IsNull(post.PrimaryTag);
        }

        [Test]
        public void GetAuthors_ReturnsTags_WhenIncludingTags()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var post = auth.GetPosts(new PostQueryParams { Limit = 1, IncludeTags = true }).Posts[0];

            Assert.IsNotNull(post.Tags);
            Assert.IsNotNull(post.PrimaryTag);

            Assert.IsNull(post.Authors);
            Assert.IsNull(post.PrimaryAuthor);
        }

        [Test]
        public void GetAuthors_ReturnsAuthorsAndTags_WhenIncludingAuthorsAndTags()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var post = auth.GetPosts(new PostQueryParams { Limit = 1, IncludeTags = true, IncludeAuthors = true }).Posts[0];

            Assert.IsNotNull(post.Tags);
            Assert.IsNotNull(post.PrimaryTag);

            Assert.IsNotNull(post.Authors);
            Assert.IsNotNull(post.PrimaryAuthor);
        }

        [Test]
        public void GetPosts_ReturnsAllPosts_WhenNoLimitIsTrue()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var postResponse = auth.GetPosts(new PostQueryParams { Limit = 1, NoLimit = true, Fields = PostFields.Id });

            Assert.GreaterOrEqual(postResponse.Posts.Count, MINIMUM_POST_COUNT_THRESHHOLD);
        }

        [Test]
        public void GetPosts_ReturnsExpectedPost_WhenOrderingByField()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var post = auth.GetPosts(new PostQueryParams { Limit = 1, Order = new List<Tuple<PostFields, OrderDirection>> { Tuple.Create(PostFields.CreatedAt, OrderDirection.asc) } }).Posts[0];

            // potentially fragile
            Assert.AreEqual("5967634699d09e0ee05c3b76", post.Id);
        }

        [Test]
        public void GetPosts_ReturnsExpectedPost_WhenGettingSecondPage()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var post = auth.GetPosts(new PostQueryParams { Page = 2, Limit = 2, Order = new List<Tuple<PostFields, OrderDirection>> { Tuple.Create(PostFields.CreatedAt, OrderDirection.asc) }, Fields = PostFields.Id }).Posts[0];

            // potentially fragile
            Assert.AreEqual("5967634699d09e0ee05c3b78", post.Id);
        }

        [Test]
        public void GetPosts_ReturnsExpectedPosts_WhenApplyingFilter()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var postResponse = auth.GetPosts(new PostQueryParams { Filter = $"slug:[{ValidPost1Slug}]" });
            Assert.AreEqual(1, postResponse.Posts.Count);

            var post = postResponse.Posts[0];

            Assert.AreEqual(ValidPost1Id, post.Id);
            Assert.AreEqual(ValidPost1Slug, post.Slug);
            Assert.AreEqual(ValidPost1Title, post.Title);
            Assert.AreEqual(ValidPost1Url, post.Url);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPosts_ThrowsException_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostContentAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPosts());
            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Unknown Content API Key", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetPosts_ReturnsNull_WhenKeyIsInvalid_AndGhostExceptionsSuppressed(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostContentAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            Assert.IsNull(auth.GetPosts());
            Assert.IsNotNull(auth.LastException);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPostById_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostById(InvalidPostId));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Post not found.", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetPostById_ReturnsNull_WhenKeyIsInvalid_AndGhostExceptionsSuppressed(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetPostById(InvalidPostId));
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPostBySlug_ThrowsGhostSharpException_WhenSlugIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostBySlug(InvalidPostSlug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Post not found.", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetPostBySlug_ReturnsNull_WhenKeyIsInvalid_AndGhostExceptionsSuppressed(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetPostBySlug(InvalidPostSlug));
        }
    }
}
