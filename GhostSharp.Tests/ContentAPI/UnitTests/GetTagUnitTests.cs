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
    public class GetTagUnitTests
    {
        private const string id = "59676341ee05c3a95";
        private const string name = "sample tag name";
        private const string slug = "sample-tag-slug";

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
        public void GetTags_ReturnsMatchingPosts()
        {
            SetupMockTag();

            var response = auth.GetTags().Tags[0];

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(name, response.Name);
            Assert.AreEqual(slug, response.Slug);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTags_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockTagGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTags());

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTags_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<TagResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<TagResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTags());

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /tags", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTags_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<TagResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetTags());

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetTags_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockTagBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTags());

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetTags_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockTagBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetTags());
        }

        [Test]
        public void GetTagById_ReturnsMatchingPost_WhenIdIsValid()
        {
            SetupMockTag();

            var response = auth.GetTagById(id);

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(name, response.Name);
            Assert.AreEqual(slug, response.Slug);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTagById_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockTagGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagById(id));

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTagById_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<TagResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<TagResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagById(id));

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /tags/{id}", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTagById_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<TagResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetTagById(id));

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetTagById_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockTagBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagById(id));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetTagById_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockTagBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetTagById(id));
        }

        [Test]
        public void GetPostBySlug_ReturnsMatchingPost_WhenSlugIsValid()
        {
            SetupMockTag();

            var response = auth.GetTagBySlug(slug);

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(name, response.Name);
            Assert.AreEqual(slug, response.Slug);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTagBySlug_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockTagGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagBySlug(slug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTagBySlug_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<TagResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<TagResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagBySlug(slug));

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /tags/slug/{slug}", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTagBySlug_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<TagResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetTagBySlug(slug));

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetTagBySlug_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockTagBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagBySlug(slug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetTagBySlug_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockTagBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetTagBySlug(slug));
        }

        protected void SetupMockTag()
        {
            var tagResponse = new
            {
                Tags = new List<Tag>
                {
                    new Tag {
                        Id = id,
                        Name = name,
                        Slug = slug
                    }
                }
            };

            var serializedTags = JsonConvert.SerializeObject(tagResponse);
            mockClient.Setup(x => x.Execute<TagResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<TagResponse> { Content = serializedTags, Data = JsonConvert.DeserializeObject<TagResponse>(serializedTags) });
        }

        protected void SetupMockTagGhostFailure()
        {
            var tagResponse = new
            {
                Errors = new List<GhostError>
                    {
                        new GhostError {
                            Message = ghostErrorMessage,
                            ErrorType = ghostErrorType
                        }
                    }
            };

            var serializedTags = JsonConvert.SerializeObject(tagResponse);
            mockClient.Setup(x => x.Execute<TagResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<TagResponse> { Content = serializedTags });
        }

        protected void SetupMockTagBothFailures()
        {
            var tagResponse = new
            {
                Errors = new List<GhostError>
                    {
                        new GhostError {
                            Message = ghostErrorMessage,
                            ErrorType = ghostErrorType
                        }
                    }
            };

            var serializedTags = JsonConvert.SerializeObject(tagResponse);
            mockClient.Setup(x => x.Execute<TagResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<TagResponse> { Content = serializedTags, ErrorException = new Exception(systemExceptionMessage) });
        }
    }
}
