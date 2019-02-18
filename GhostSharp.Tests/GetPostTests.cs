using GhostSharp;
using GhostSharp.Entities;
using NUnit.Framework;

namespace GhostSharpTests
{
    [TestFixture]
    public class GetPostTests : TestBase
    {
        GhostAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAPI(Host, ValidApiKey);
        }

        [Test]
        public void GetPostById_ReturnsMatchingPost_WhenIdIsValid()
        {
            var actualPostId = auth.GetPostById(ValidPostId).Id;

            Assert.AreEqual(ValidPostId, actualPostId);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPostById_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostById(InvalidPostId));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Validation (matches) failed for id", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetPostById_DoesNotThrow_ReturnsNull_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetPostById(InvalidPostId));
        }

        [Test]
        public void GetPostBySlug_ReturnsMatchingPost_WhenSlugIsValid()
        {
            var actualSlug = auth.GetPostBySlug(ValidPostSlug).Slug;

            Assert.AreEqual(ValidPostSlug, actualSlug);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPostBySlug_ThrowsGhostSharpException_WhenSlugIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostBySlug(InvalidPostSlug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Validation (isSlug) failed for slug", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetPostBySlug_DoesNotThrow_ReturnsNull_WhenSlugIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetPostBySlug(InvalidPostSlug));
        }
    }
}
