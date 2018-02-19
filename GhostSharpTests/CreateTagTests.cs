using System;
using GhostSharp;
using GhostSharp.Entities;
using Xunit;

namespace GhostSharpTests
{
    public class CreateTagTests : TestBase, IDisposable
    {
        readonly GhostAPI auth;
        Tag createdTag;

        public CreateTagTests()
        {
            createdTag = null;
            auth = new GhostAPI(Url, AuthToken);
        }

        [Fact]
        public void CreateTag_ReturnsTag_WhenTagCreated()
        {
            string tagName = "tag_name_that_likely_does_not_exist";
            string tagSlug = "tag_slug_that_hopefully_does_not_exist";
            string tagDesc = "This is a wonderful description.";

            createdTag = auth.CreateTag(tagName, tagSlug, tagDesc);

            Assert.Equal(tagName, createdTag.Name);
            Assert.Equal(tagSlug, createdTag.Slug);
            Assert.Equal(tagDesc, createdTag.Description);
        }

        [Fact]
        public void CreateTag_ReturnsTagWithSlugMatchingName_WhenSlugOmitted()
        {
            string tagName = "tag_name_that_likely_does_not_exist";
            string tagDesc = "This is a wonderful description.";

            createdTag = auth.CreateTag(tagName, null, tagDesc);

            Assert.Equal(tagName, createdTag.Name);
            Assert.Equal(tagName, createdTag.Slug);
            Assert.Equal(tagDesc, createdTag.Description);
        }

        [Fact]
        public void CreateTag_ReturnsTagWithEmptyDescription_WhenDescriptionOmitted()
        {
            string tagName = "tag_name_that_likely_does_not_exist";
            string tagSlug = "tag_slug_that_hopefully_does_not_exist";

            createdTag = auth.CreateTag(tagName, tagSlug, null);

            Assert.Equal(tagName, createdTag.Name);
            Assert.Equal(tagSlug, createdTag.Slug);
            Assert.Equal("", createdTag.Description);
        }

        [Fact]
        public void CreateTag_ThrowsException_WhenNameOmitted_AndSuppressionLevelNone()
        {
            auth.SuppressionLevel = SuppressionLevel.None;
            var ex = Assert.Throws<GhostSharpException>(() => createdTag = auth.CreateTag(null, "slug", "description"));

            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("ValidationError", ex.Errors[0].ErrorType);
            Assert.StartsWith("Value in [tags.name] cannot be blank", ex.Errors[0].Message);
        }

        [Theory]
        [InlineData(SuppressionLevel.GhostOnly)]
        [InlineData(SuppressionLevel.All)]
        public void CreateTag_ReturnsNull_WhenNameOmitted_AndSuppressionLevelNotNone(SuppressionLevel level)
        {
            auth.SuppressionLevel = level;
            createdTag = auth.CreateTag(null, "slug", "description");

            Assert.Null(createdTag);
            Assert.NotNull(auth.LastException);
        }

        public void Dispose()
        {
            if (createdTag != null)
                auth.DeleteTagBySlug(createdTag.Slug);
            createdTag = null;
        }
    }
}
