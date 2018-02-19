using GhostSharp;
using GhostSharp.Entities;
using Xunit;

namespace GhostSharpTests
{
    public class DeleteUserTests : TestBase
    {
        readonly GhostAPI auth;
            
        const string invalidUserId = "invalid_user_id_234234kjk2j34";
        const string invalidUserSlug = "invalid_user_slug_does_not_exist";

        public DeleteUserTests()
        {
            auth = new GhostAPI(Url, AuthToken);
        }

        // It's not possible to create users via the API (resource not found),
        // so you'd have to create a user manually, get the ID, and then run this test.
        //[Fact]
        public void DeleteUserById_ReturnsTrue_WhenUserExists()
        {
            var validUserId = "???";
      
            Assert.True(auth.DeleteUserById(validUserId));
        }

        [Fact]
        public void DeleteUserById_ThrowsException_WhenUserDoesNotExist_AndSuppressionLevelNone()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.DeleteUserById(invalidUserId));

            Assert.NotEmpty(ex.Errors);
            Assert.Equal("ValidationError", ex.Errors[0].ErrorType);
        }

        // It's not possible to create users via the API (resource not found),
        // so you'd have to create a user manually, get the Slug, and then run this test.
        //[Fact]
        public void DeleteUserBySlug_ReturnsTrue_WhenUserExists()
        {
            var validUserSlug = "???";
     
            Assert.True(auth.DeleteUserBySlug(validUserSlug));
        }

        [Fact]
        public void DeleteUserBySlug_ThrowsException_WhenUserDoesNotExist_AndSuppressionLevelNone()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.DeleteUserBySlug(invalidUserSlug));

            // The error is different than DELETE by ID, because DELETE by Slug is unsupported
            // and requires a GET by Slug first. When the GET fails, it throws a "NotFoundError".

            Assert.NotEmpty(ex.Errors);
            Assert.Equal("NotFoundError", ex.Errors[0].ErrorType);
            Assert.StartsWith("User not found", ex.Errors[0].Message);
        }

        [Theory]
        [InlineData(SuppressionLevel.GhostOnly)]
        [InlineData(SuppressionLevel.All)]
        public void DeleteUserById_ReturnsFalse_WhenUserDoesNotExist_AndSuppressionLevelNotNone(SuppressionLevel level)
        {
            auth.SuppressionLevel = level;

            Assert.False(auth.DeleteUserById(invalidUserId));
            Assert.NotNull(auth.LastException);
        }
    }
}
