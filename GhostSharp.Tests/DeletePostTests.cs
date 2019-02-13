using GhostSharp;
using GhostSharp.Entities;
using NUnit.Framework;

namespace GhostSharpTests
{
    [TestFixture]
    public class DeletePostTests : TestBase
    {
        readonly GhostAPI auth;
       
        const string unlikelyTitle = "some-really-random-title-that-i-hope-no-one-uses-234566";
        const string nonExistentPostId = "its_highly_unlikely_this_post_id_actually_exists";

        public DeletePostTests()
        {
            auth = new GhostAPI(Url, AuthToken);
        }

        [Test]
        public void DeletePostById_ReturnsTrue_WhenIdIsValid()
        {
            var postId = auth.CreatePost(new Post { Title = unlikelyTitle }).Id;
         
            Assert.True(auth.DeletePostById(postId));
        }

        [Test]
        public void DeletePostBySlug_ReturnsTrue_WhenSlugIsValid()
        {
            var slug = auth.CreatePost(new Post { Title = unlikelyTitle }).Slug;
          
            Assert.True(auth.DeletePostBySlug(slug));
        }

        [Test]
        public void DeletePostById_ThrowsException_WhenIdIsInvalid_AndSuppressionLevelNone()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.DeletePostById(nonExistentPostId));
            Assert.IsNotEmpty(ex.Errors);
            StringAssert.StartsWith("ValidationError", ex.Errors[0].ErrorType);
            StringAssert.StartsWith("Validation (matches) failed for id", ex.Errors[0].Message);
        }

        [Test]
        public void DeletePostById_ReturnsFalse_WhenIdIsInvalid_AndSuppressionLevelGhostOnly()
        {
            auth.SuppressionLevel = SuppressionLevel.GhostOnly;
            Assert.False(auth.DeletePostById(nonExistentPostId));
        }

        [Test]
        public void DeletePostById_ReturnsFalse_WhenIdIsInvalid_AndSuppressionLevelAll()
        {
            auth.SuppressionLevel = SuppressionLevel.All;
            Assert.False(auth.DeletePostById(nonExistentPostId));
        }
    }
}
