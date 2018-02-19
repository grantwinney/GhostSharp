using GhostSharp;
using GhostSharp.Entities;
using Xunit;

namespace GhostSharpTests
{
    public class DeletePostTests : TestBase
    {
        readonly GhostAPI auth;
       
        const string unlikelyTitle = "some-really-random-title-that-i-hope-no-one-uses-234566";
        const string nonExistentPostId = "its_highly_unlikely_this_post_id_actually_exists";

        public DeletePostTests()
        {
            auth = new GhostAPI(Url, AuthToken);
        }

        [Fact]
        public void DeletePostById_ReturnsTrue_WhenIdIsValid()
        {
            var postId = auth.CreatePost(new Post { Title = unlikelyTitle }).Id;
         
            Assert.True(auth.DeletePostById(postId));
        }

        [Fact]
        public void DeletePostBySlug_ReturnsTrue_WhenSlugIsValid()
        {
            var slug = auth.CreatePost(new Post { Title = unlikelyTitle }).Slug;
          
            Assert.True(auth.DeletePostBySlug(slug));
        }

        [Fact]
        public void DeletePostById_ThrowsException_WhenIdIsInvalid_AndSuppressionLevelNone()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.DeletePostById(nonExistentPostId));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("ValidationError", ex.Errors[0].ErrorType);
            Assert.StartsWith("Validation (matches) failed for id", ex.Errors[0].Message);
        }

        [Fact]
        public void DeletePostById_ReturnsFalse_WhenIdIsInvalid_AndSuppressionLevelGhostOnly()
        {
            auth.SuppressionLevel = SuppressionLevel.GhostOnly;
            Assert.False(auth.DeletePostById(nonExistentPostId));
        }

        [Fact]
        public void DeletePostById_ReturnsFalse_WhenIdIsInvalid_AndSuppressionLevelAll()
        {
            auth.SuppressionLevel = SuppressionLevel.All;
            Assert.False(auth.DeletePostById(nonExistentPostId));
        }
    }
}
