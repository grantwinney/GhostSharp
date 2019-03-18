using GhostSharp.Entities;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;

namespace GhostSharp.Tests.UnitTests
{
    [TestFixture]
    public class GetPageUnitTests
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
        public void GetPages_ReturnsMatchingPages()
        {
            SetupMockPage();

            var response = auth.GetPages().Pages[0];

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(title, response.Title);
            Assert.AreEqual(slug, response.Slug);
            Assert.AreEqual(html, response.Html);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPages_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockPageGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPages());

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPages_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PageResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PageResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPages());

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /pages", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPages_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PageResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetPages());

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetPages_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockPageBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPages());

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetPages_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockPageBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetPages());
        }

        [Test]
        public void GetPageById_ReturnsMatchingPage_WhenIdIsValid()
        {
            SetupMockPage();
            
            var response = auth.GetPageById(id);

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(title, response.Title);
            Assert.AreEqual(slug, response.Slug);
            Assert.AreEqual(html, response.Html);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPageById_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockPageGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPageById(id));

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPageById_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PageResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PageResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPageById(id));

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /pages/{id}", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPageById_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PageResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetPageById(id));

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetPageById_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockPageBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPageById(id));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetPageById_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockPageBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetPageById(id));
        }

        [Test]
        public void GetPageBySlug_ReturnsMatchingPage_WhenSlugIsValid()
        {
            SetupMockPage();

            var response = auth.GetPageBySlug(slug);

            Assert.AreEqual(id, response.Id);
            Assert.AreEqual(title, response.Title);
            Assert.AreEqual(slug, response.Slug);
            Assert.AreEqual(html, response.Html);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPageBySlug_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            SetupMockPageGhostFailure();
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPageBySlug(slug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPageBySlug_ThrowsGhostSharpException_WhenRequestThrowsException(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PageResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PageResponse> { ErrorException = new Exception(ghostErrorMessage) });
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPageBySlug(slug));

            Assert.IsEmpty(ex.Errors);
            Assert.IsNotNull(auth.LastException);
            StringAssert.StartsWith($"Unable to GET /pages/slug/{slug}", ex.Message);
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPageBySlug_ThrowsException_WhenUnexpectedError(ExceptionLevel exceptionLevel)
        {
            mockClient.Setup(x => x.Execute<PageResponse>(It.IsAny<RestRequest>()))
                      .Throws(new Exception(systemExceptionMessage));
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<Exception>(() => auth.GetPageBySlug(slug));

            Assert.AreEqual(systemExceptionMessage, ex.Message);
        }

        [Test]
        public void GetPageBySlug_PrefersGhostException_WhenGhostAndRegularExceptionsAreBothPossible()
        {
            SetupMockPageBothFailures();
            auth.ExceptionLevel = ExceptionLevel.All;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPageBySlug(slug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual(ghostErrorMessage, ex.Message);
            Assert.AreEqual(ghostErrorType, ex.Errors[0].ErrorType);
        }

        [Test]
        public void GetPageBySlug_ReturnsNull_WhenIdIsInvalid_ButExceptionLevelSetToNone()
        {
            SetupMockPageBothFailures();
            auth.ExceptionLevel = ExceptionLevel.None;

            Assert.IsNull(auth.GetPageBySlug(slug));
        }

        protected void SetupMockPage()
        {
            var PageResponse = new
            {
                Pages = new List<Page>
                {
                    new Page {
                        Id = id,
                        Page = false,
                        Title = title,
                        Slug = slug,
                        Html = html
                    }
                }
            };

            var serializedPages = JsonConvert.SerializeObject(PageResponse);
            mockClient.Setup(x => x.Execute<PageResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PageResponse> { Content = serializedPages, Data = JsonConvert.DeserializeObject<PageResponse>(serializedPages) });
        }

        protected void SetupMockPageGhostFailure()
        {
            var PageResponse = new
            {
                Errors = new List<GhostError>
                    {
                        new GhostError {
                            Message = ghostErrorMessage,
                            ErrorType = ghostErrorType
                        }
                    }
            };

            var serializedPages = JsonConvert.SerializeObject(PageResponse);
            mockClient.Setup(x => x.Execute<PageResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PageResponse> { Content = serializedPages });
        }

        protected void SetupMockPageBothFailures()
        {
            var PageResponse = new
            {
                Errors = new List<GhostError>
                    {
                        new GhostError {
                            Message = ghostErrorMessage,
                            ErrorType = ghostErrorType
                        }
                    }
            };

            var serializedPages = JsonConvert.SerializeObject(PageResponse);
            mockClient.Setup(x => x.Execute<PageResponse>(It.IsAny<RestRequest>()))
                      .Returns(new RestResponse<PageResponse> { Content = serializedPages, ErrorException = new Exception(systemExceptionMessage) });
        }
    }
}
