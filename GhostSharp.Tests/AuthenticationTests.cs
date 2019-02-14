using GhostSharp;
using GhostSharp.Entities;
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

            var postResponse = auth.GetPosts(new PostQueryParams { Limit = 2, Fields = "id" });

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
        public void GetTags_ReturnsTags_WhenKeyIsValid()
        {
            var auth = new GhostAPI(Host, ValidApiKey);

            var tagResponse = auth.GetTags(new TagQueryParams { Limit = 2, Fields = "id" });

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

            var authorResponse = auth.GetAuthors(new AuthorQueryParams { Limit = 1, Fields = "id" });

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
    }
}
