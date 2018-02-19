using GhostSharp;
using GhostSharp.Entities;
using Xunit;

namespace GhostSharpTests
{
    public class DeleteTagTests : TestBase
    {
        readonly GhostAPI auth;

        const string tagName = "some_completely_random_tag_name";
        const string tagSlug = "some_completely_random_tag_slug";
        const string tagDesc = "some_completely_random_tag_description";

        const string nonExistentTagId = "its_highly_unlikely_this_tag_id_actually_exists";

        public DeleteTagTests()
        {
            auth = new GhostAPI(Url, AuthToken);
        }

        [Fact]
        public void DeleteTagBySlug_ReturnsTrue_WhenTagDeletedById()
        {
            var tag = auth.CreateTag(tagName, tagSlug, tagDesc);
         
            Assert.True(auth.DeleteTagById(tag.Id));
        }

        [Fact]
        public void DeleteTag_ReturnsTrue_WhenTagDeletedBySlug()
        {
            var tag = auth.CreateTag(tagName, tagSlug, tagDesc);
      
            Assert.True(auth.DeleteTagBySlug(tagSlug));
        }

        [Fact]
        public void DeleteTag_ThrowsException_WhenTagDoesNotExist_AndSuppressionLevelNone()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.DeleteTagById(nonExistentTagId));

            Assert.NotEmpty(ex.Errors);
            Assert.Equal("ValidationError", ex.Errors[0].ErrorType);
            Assert.Equal("Validation (matches) failed for id", ex.Errors[0].Message);
        }

        [Fact]
        public void DeleteTag_ReturnsFalse_WhenTagDoesNotExist_AndSuppressionLevelGhostOnly()
        {
            auth.SuppressionLevel = SuppressionLevel.GhostOnly;
            Assert.False(auth.DeleteTagById(nonExistentTagId));
        }

        [Fact]
        public void DeleteTag_ReturnsFalse_WhenTagDoesNotExist_AndSuppressionLevelAll()
        {
            auth.SuppressionLevel = SuppressionLevel.All;
            Assert.False(auth.DeleteTagById(nonExistentTagId));
        }
    }
}
