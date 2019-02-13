using System;
using GhostSharp;
using GhostSharp.Entities;
using NUnit.Framework;

namespace GhostSharpTests
{
    [TestFixture]
    public class GetTagTests : TestBase, IDisposable
    {
        readonly GhostAPI auth;

        Tag createdTag;
        const string tagName = "some_completely_random_tag_name";
        const string tagSlug = "some_completely_random_tag_slug";
        const string tagDesc = "some_completely_random_tag_description";

        public GetTagTests()
        {
            createdTag = null;
            auth = new GhostAPI(Url, AuthToken);
        }
        
        [Test]
        public void GetTagById_ReturnsMatchingTag_WhenIdIsValid()
        {
            createdTag = auth.CreateTag(tagName, tagSlug, tagDesc);

            var tag = auth.GetTagById(createdTag.Id);

            Assert.AreEqual(createdTag.Id, tag.Id);
        }

        [Test]
        public void GetTagById_ThrowsException_WhenIdIsInvalid()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagById("invalid_id"));
            Assert.IsNotEmpty(ex.Errors);
            StringAssert.StartsWith("Tag not found", ex.Errors[0].Message);
        }

        [Test]
        public void GetTagBySlug_ReturnsMatchingTag_WhenSlugIsValid()
        {
            createdTag = auth.CreateTag(tagName, tagSlug, tagDesc);
        
            var tag = auth.GetTagBySlug(tagSlug);

            Assert.AreEqual(tagSlug, tag.Slug);
        }

        [Test]
        public void GetTagBySlug_ThrowsException_WhenSlugIsInvalid()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagBySlug("invalid_slug"));
            Assert.IsNotEmpty(ex.Errors);
            StringAssert.StartsWith("Tag not found", ex.Errors[0].Message);
        }

        public void Dispose()
        {
            if (createdTag != null)
                auth.DeleteTagById(createdTag.Id);
            createdTag = null;
        }
    }
}
