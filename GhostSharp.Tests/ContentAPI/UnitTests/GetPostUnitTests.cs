using GhostSharp.Entities;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;

namespace GhostSharp.Tests.ContentAPI.UnitTests
{
    [TestFixture]
    public class GetPostUnitTests
    {
        private const string id = "5c815afe8651f1834d";
        private const string title = "sample title";
        private const string slug = "sample-slug";
        private const string html = "<p>Sample body</p>";

        private const string systemExceptionMessage = "A system exception was thrown!";
        private const string ghostErrorMessage = "A Ghost exception was thrown!";
        private const string ghostErrorType = "UnauthorizedError";

        private GhostAPI auth;
        private Mock<IRestClient> mockClient;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostContentAPI("http://invalid_uri.com", "invalid_key");
            mockClient = new Mock<IRestClient>(MockBehavior.Strict);
            auth.Client = mockClient.Object;
        }

        [TearDown]
        public void TearDown()
        {
            mockClient.Reset();
        }

        [Test]
        public void GetPosts_ReturnsMatchingPosts()
        {
            SetupMockPost();

            var response = auth.GetPosts().Posts[0];

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(title, response.Title);
            Assert.AreEqual(slug, response.Slug);
            Assert.AreEqual(html, response.Html);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPosts_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockPostGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPosts());

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPosts_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PostResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PostResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPosts());

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /posts", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPosts_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PostResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetPosts());

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetPosts_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockPostBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPosts());

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetPosts_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockPostBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetPosts());
        }

        [Test]
        public void GetPostById_ReturnsMatchingPost_WhenIdIsValid()
        {
            SetupMockPost();
            
            var response = auth.GetPostById(id);

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(title, response.Title);
            Assert.AreEqual(slug, response.Slug);
            Assert.AreEqual(html, response.Html);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPostById_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockPostGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostById(id));

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPostById_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PostResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PostResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostById(id));

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /posts/{id}", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPostById_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PostResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetPostById(id));

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetPostById_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockPostBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostById(id));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetPostById_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockPostBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetPostById(id));
        }

        [Test]
        public void GetPostBySlug_ReturnsMatchingPost_WhenSlugIsValid()
        {
            SetupMockPost();

            var response = auth.GetPostBySlug(slug);

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(title, response.Title);
            Assert.AreEqual(slug, response.Slug);
            Assert.AreEqual(html, response.Html);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPostBySlug_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockPostGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostBySlug(slug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPostBySlug_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PostResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PostResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostBySlug(slug));

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /posts/slug/{slug}", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPostBySlug_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PostResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetPostBySlug(slug));

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetPostBySlug_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockPostBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostBySlug(slug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetPostBySlug_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockPostBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetPostBySlug(slug));
        }

        protected void SetupMockPost()
        {
            var postResponse = new
            {
                Posts = new List<Post>
                {
                    new Post {
                        Id = id,
                        Page = false,
                        Title = title,
                        Slug = slug,
                        Html = html
                    }
                }
            };

            var serializedPosts = JsonConvert.SerializeObject(postResponse);
            mockClient.Setup(x => x.Execute<PostResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PostResponse> { Content = serializedPosts, Data = JsonConvert.DeserializeObject<PostResponse>(serializedPosts) });
        }

        protected void SetupMockPostGhostFailure()
        {
            var postResponse = new
            {
                Errors = new List<GhostError>
                    {
                        new GhostError {
                            Message = ghostErrorMessage,
                            ErrorType = ghostErrorType
                        }
                    }
            };

            var serializedPosts = JsonConvert.SerializeObject(postResponse);
            mockClient.Setup(x => x.Execute<PostResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PostResponse> { Content = serializedPosts });
        }

        protected void SetupMockPostBothFailures()
        {
            var postResponse = new
            {
                Errors = new List<GhostError>
                    {
                        new GhostError {
                            Message = ghostErrorMessage,
                            ErrorType = ghostErrorType
                        }
                    }
            };

            var serializedPosts = JsonConvert.SerializeObject(postResponse);
            mockClient.Setup(x => x.Execute<PostResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PostResponse> { Content = serializedPosts, ErrorException = new Exception(systemExceptionMessage) });
        }
    }
}
