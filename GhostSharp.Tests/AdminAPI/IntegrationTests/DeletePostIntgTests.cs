using GhostSharp.Entities;
using GhostSharp.Enums;
using NUnit.Framework;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class DeletePostIntgTests : TestBase
    {
        private GhostAdminAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [Test]
        public void DeletePost_ReturnsTrue_WhenPostIdValid()
        {
            var newPostId = auth.CreatePost(new Post { Title = "This is a sample post for testing delete functionality" }).Id;

            Assert.IsTrue(auth.DeletePost(newPostId));
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.None)]
        [Test]
        public void DeletePost_ReturnsFalse_WhenPostIdInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsFalse(auth.DeletePost(InvalidPostId));
        }

        [TestCase(ExceptionLevel.All)]
        [TestCase(ExceptionLevel.Ghost)]
        [Test]
        public void DeletePost_Throws_WhenPostIdInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.DeletePost(InvalidPostId));

            Assert.That(ex.Message.StartsWith("Resource not found error, cannot delete post."));
        }
    }
}