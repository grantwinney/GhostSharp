using GhostSharp;
using GhostSharp.Entities;
using Xunit;

namespace GhostSharpTests
{
    public class GetUserTests : TestBase
    {
        readonly GhostAPI auth;

        public GetUserTests()
        {
            auth = new GhostAPI(Url, AuthToken);
        }

        [Fact]
        public void GetUserById_ReturnsMatchingUser_WhenIdIsValid()
        {
            var user = auth.GetUserById(UserId);

            Assert.Equal(UserId, user.Id);
        }

        [Fact]
        public void GetUserById_ThrowsException_WhenIdIsInvalid()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetUserById("invalid_id"));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("User not found", ex.Errors[0].Message);
        }

        [Fact]
        public void GetUserBySlug_ReturnsMatchingUser_WhenSlugIsValid()
        {
            var user = auth.GetUserBySlug(UserSlug);

            Assert.Equal(UserSlug, user.Slug);
        }

        [Fact]
        public void GetUserBySlug_ThrowsException_WhenSlugIsInvalid()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetUserBySlug("invalid_slug"));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("User not found", ex.Errors[0].Message);
        }

        [Fact]
        public void GetMyProfile_ReturnsUserMatchingCredentials()
        {
            var user = auth.GetMyProfile();

            Assert.Equal(UserId, user.Id);
        }
    }
}
