using System;
using GhostSharp;
using GhostSharp.Entities;
using NUnit.Framework;

namespace GhostSharpTests
{
    [TestFixture]
    public class GetTagTests : TestBase
    {
        readonly GhostAPI auth;

        public GetTagTests()
        {
            auth = new GhostAPI(Host, ValidApiKey);
        }
        
        [Test]
        public void GetTagById_ReturnsMatchingTag_WhenIdIsValid()
        {
            var tag = auth.GetTagById(ValidTagId);

            Assert.AreEqual(ValidTagId, tag.Id);
        }


        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTagById_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagById(InvalidTagId));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Validation (matches) failed for id", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetTagById_DoesNotThrow_ReturnsNull_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetTagById(InvalidTagId));
        }


        [Test]
        public void GetTagBySlug_ReturnsMatchingTag_WhenSlugIsValid()
        {
            var tag = auth.GetTagBySlug(ValidTagSlug);

            Assert.AreEqual(ValidTagSlug, tag.Slug);
        }


        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTagBySlug_ThrowsGhostSharpException_WhenSlugIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagBySlug(InvalidTagSlug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Validation (isSlug) failed for slug", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetTagBySlug_DoesNotThrow_ReturnsNull_WhenSlugIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetTagBySlug(InvalidTagSlug));
        }
    }
}
