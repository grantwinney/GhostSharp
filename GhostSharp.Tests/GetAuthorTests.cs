using GhostSharp;
using GhostSharp.Entities;
using NUnit.Framework;

namespace GhostSharpTests
{
    [TestFixture]
    public class GetAuthorTests : TestBase
    {
        readonly GhostAPI auth;

        public GetAuthorTests()
        {
            auth = new GhostAPI(Host, ValidApiKey);
        }

        [Test]
        public void GetAuthorById_ReturnsMatchingAuthor_WhenIdIsValid()
        {
            var author = auth.GetAuthorById(ValidAuthorId);

            Assert.AreEqual(ValidAuthorId, author.Id);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthorById_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthorById(InvalidAuthorId));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Validation (matches) failed for id", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetAuthorById_DoesNotThrow_ReturnsNull_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetAuthorById(InvalidAuthorId));
        }

        [Test]
        public void GetAuthorBySlug_ReturnsMatchingAuthor_WhenSlugIsValid()
        {
            var author = auth.GetAuthorBySlug(ValidAuthorSlug);

            Assert.AreEqual(ValidAuthorId, author.Id);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetAuthorBySlug_ThrowsGhostSharpException_WhenSlugIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthorBySlug(InvalidAuthorSlug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Validation (isSlug) failed for slug", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetAuthorBySlug_DoesNotThrow_ReturnsNull_WhenSlugIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetAuthorBySlug(InvalidAuthorSlug));
        }
    }
}
