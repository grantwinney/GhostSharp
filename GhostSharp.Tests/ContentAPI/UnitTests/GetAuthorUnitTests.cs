using GhostSharp.Entities;
using GhostSharp.Enums;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;

namespace GhostSharp.Tests.ContentAPI.UnitTests
{
    [TestFixture]
    public class GetAuthorUnitTests
    {
        private const string id = "596bg39cdfa95";
        private const string bio = "this is a sample bio";
        private const string slug = "sample-author-slug";

        private const string systemExceptionMessage = "A system exception was thrown!";
        private const string ghostErrorMessage = "A Ghost exception was thrown!";
        private const string ghostErrorType = "UnauthorizedError";

        private GhostContentAPI auth;
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
        public void GetAuthors_ReturnsMatchingPosts()
        {
            SetupMockAuthor();

            var response = auth.GetAuthors().Authors[0];

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(bio, response.Bio);
            Assert.AreEqual(slug, response.Slug);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthors_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockAuthorGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthors());

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthors_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<AuthorResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<AuthorResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthors());

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /authors", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthors_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<AuthorResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetAuthors());

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetAuthors_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockAuthorBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthors());

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetAuthors_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockAuthorBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetAuthors());
        }

        [Test]
        public void GetAuthorById_ReturnsMatchingPost_WhenIdIsValid()
        {
            SetupMockAuthor();

            var response = auth.GetAuthorById(id);

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(bio, response.Bio);
            Assert.AreEqual(slug, response.Slug);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthorById_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockAuthorGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthorById(id));

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthorById_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<AuthorResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<AuthorResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthorById(id));

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /authors/{id}", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthorById_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<AuthorResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetAuthorById(id));

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetAuthorById_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockAuthorBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthorById(id));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetAuthorById_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockAuthorBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetAuthorById(id));
        }

        [Test]
        public void GetPostBySlug_ReturnsMatchingPost_WhenSlugIsValid()
        {
            SetupMockAuthor();

            var response = auth.GetAuthorBySlug(slug);

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(bio, response.Bio);
            Assert.AreEqual(slug, response.Slug);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthorBySlug_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockAuthorGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthorBySlug(slug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthorBySlug_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<AuthorResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<AuthorResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthorBySlug(slug));

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /authors/slug/{slug}", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthorBySlug_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<AuthorResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetAuthorBySlug(slug));

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetAuthorBySlug_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockAuthorBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthorBySlug(slug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetAuthorBySlug_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockAuthorBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetAuthorBySlug(slug));
        }

        protected void SetupMockAuthor()
        {
            var AuthorResponse = new
            {
                Authors = new List<Author>
                {
                    new Author {
                        Id = id,
                        Bio = bio,
                        Slug = slug
                    }
                }
            };

            var serializedAuthors = JsonConvert.SerializeObject(AuthorResponse);
            mockClient.Setup(x => x.Execute<AuthorResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<AuthorResponse> { Content = serializedAuthors, Data = JsonConvert.DeserializeObject<AuthorResponse>(serializedAuthors) });
        }

        protected void SetupMockAuthorGhostFailure()
        {
            var AuthorResponse = new
            {
                Errors = new List<GhostError>
                    {
                        new GhostError {
                            Message = ghostErrorMessage,
                            ErrorType = ghostErrorType
                        }
                    }
            };

            var serializedAuthors = JsonConvert.SerializeObject(AuthorResponse);
            mockClient.Setup(x => x.Execute<AuthorResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<AuthorResponse> { Content = serializedAuthors });
        }

        protected void SetupMockAuthorBothFailures()
        {
            var AuthorResponse = new
            {
                Errors = new List<GhostError>
                    {
                        new GhostError {
                            Message = ghostErrorMessage,
                            ErrorType = ghostErrorType
                        }
                    }
            };

            var serializedAuthors = JsonConvert.SerializeObject(AuthorResponse);
            mockClient.Setup(x => x.Execute<AuthorResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<AuthorResponse> { Content = serializedAuthors, ErrorException = new Exception(systemExceptionMessage) });
        }
    }
}
