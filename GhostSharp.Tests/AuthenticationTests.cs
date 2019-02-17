using GhostSharp;
using GhostSharp.Entities;
using GhostSharp.Enums;
using GhostSharp.QueryParams;
using NUnit.Framework;

namespace GhostSharpTests
{
    [TestFixture]
    public class AuthenticationTests : TestBase
    {
        [Test]
        public void GetPosts_ReturnsPosts_WhenKeyIsValid()
        {
            var auth = new GhostAPI(Host, ValidApiKey);

            var postResponse = auth.GetPosts(new PostQueryParams { Limit = 2, Fields = PostFields.Id });

            Assert.AreEqual(2, postResponse.Posts.Count);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPosts_ThrowsException_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPosts());
            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Unknown Content API Key", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetPosts_DoesNotThrow_ReturnsNull_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            Assert.IsNull(auth.GetPosts());
            Assert.IsNotNull(auth.LastException);
        }

        [Test]
        public void GetPages_ReturnsPages_WhenKeyIsValid()
        {
            var auth = new GhostAPI(Host, ValidApiKey);

            var pageResponse = auth.GetPages(new PageQueryParams { Limit = 2, Fields = PostFields.Id });

            Assert.AreEqual(2, pageResponse.Pages.Count);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPages_ThrowsException_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPages());
            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Unknown Content API Key", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetPages_DoesNotThrow_ReturnsNull_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            Assert.IsNull(auth.GetPages());
            Assert.IsNotNull(auth.LastException);
        }

        [Test]
        public void GetTags_ReturnsTags_WhenKeyIsValid()
        {
            var auth = new GhostAPI(Host, ValidApiKey);

            var tagResponse = auth.GetTags(new TagQueryParams { Limit = 2, Fields = TagFields.Id });

            Assert.AreEqual(2, tagResponse.Tags.Count);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTags_ThrowsException_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTags());
            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Unknown Content API Key", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetTags_DoesNotThrow_ReturnsNull_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            Assert.IsNull(auth.GetTags());
            Assert.IsNotNull(auth.LastException);
        }

        [Test]
        public void GetAuthors_ReturnsAuthors_WhenKeyIsValid()
        {
            var auth = new GhostAPI(Host, ValidApiKey);

            var authorResponse = auth.GetAuthors(new AuthorQueryParams { Limit = 1, Fields = AuthorFields.Id });

            Assert.AreEqual(1, authorResponse.Authors.Count);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthors_ThrowsException_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthors());
            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Unknown Content API Key", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetAuthors_DoesNotThrow_ReturnsNull_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            Assert.IsNull(auth.GetAuthors());
            Assert.IsNotNull(auth.LastException);
        }

        [Test]
        public void GetSettings_ReturnsSettings_WhenKeyIsValid()
        {
            var auth = new GhostAPI(Host, ValidApiKey);

            Assert.AreEqual(SiteTitle, auth.GetSettings().Title);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetSettings_ThrowsException_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetSettings());
            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Unknown Content API Key", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetSettings_DoesNotThrow_ReturnsNull_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            Assert.IsNull(auth.GetSettings());
            Assert.IsNotNull(auth.LastException);
        }
    }
}
