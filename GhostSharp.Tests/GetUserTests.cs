using GhostSharp;
using GhostSharp.Entities;
using NUnit.Framework;

namespace GhostSharpTests
{
    [TestFixture]
    public class GetUserTests : TestBase
    {
        readonly GhostAPI auth;

        public GetUserTests()
        {
            auth = new GhostAPI(Url, AuthToken);
        }

        [Test]
        public void GetUserById_ReturnsMatchingUser_WhenIdIsValid()
        {
            var user = auth.GetUserById(UserId);

            Assert.AreEqual(UserId, user.Id);
        }

        [Test]
        public void GetUserById_ThrowsException_WhenIdIsInvalid()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetUserById("invalid_id"));
            Assert.IsNotEmpty(ex.Errors);
            StringAssert.StartsWith("User not found", ex.Errors[0].Message);
        }

        [Test]
        public void GetUserBySlug_ReturnsMatchingUser_WhenSlugIsValid()
        {
            var user = auth.GetUserBySlug(UserSlug);

            Assert.AreEqual(UserSlug, user.Slug);
        }

        [Test]
        public void GetUserBySlug_ThrowsException_WhenSlugIsInvalid()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetUserBySlug("invalid_slug"));
            Assert.IsNotEmpty(ex.Errors);
            StringAssert.StartsWith("User not found", ex.Errors[0].Message);
        }

        [Test]
        public void GetMyProfile_ReturnsUserMatchingCredentials()
        {
            var user = auth.GetMyProfile();

            Assert.AreEqual(UserId, user.Id);
        }
    }
}
