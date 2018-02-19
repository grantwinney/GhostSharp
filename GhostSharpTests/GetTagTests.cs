using System;
using GhostSharp;
using GhostSharp.Entities;
using Xunit;

namespace GhostSharpTests
{
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
        
        [Fact]
        public void GetTagById_ReturnsMatchingTag_WhenIdIsValid()
        {
            createdTag = auth.CreateTag(tagName, tagSlug, tagDesc);

            var tag = auth.GetTagById(createdTag.Id);

            Assert.Equal(createdTag.Id, tag.Id);
        }

        [Fact]
        public void GetTagById_ThrowsException_WhenIdIsInvalid()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagById("invalid_id"));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Tag not found", ex.Errors[0].Message);
        }

        [Fact]
        public void GetTagBySlug_ReturnsMatchingTag_WhenSlugIsValid()
        {
            createdTag = auth.CreateTag(tagName, tagSlug, tagDesc);
        
            var tag = auth.GetTagBySlug(tagSlug);

            Assert.Equal(tagSlug, tag.Slug);
        }

        [Fact]
        public void GetTagBySlug_ThrowsException_WhenSlugIsInvalid()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagBySlug("invalid_slug"));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Tag not found", ex.Errors[0].Message);
        }

        public void Dispose()
        {
            if (createdTag != null)
                auth.DeleteTagById(createdTag.Id);
            createdTag = null;
        }
    }
}
