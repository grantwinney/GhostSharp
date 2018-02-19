using GhostSharp;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using Xunit;

namespace GhostSharpTests
{
    public class AuthenticationTests : TestBase
    {
        [Fact]
        public void GetAuthToken_ReturnsToken_WhenCredentialsValid()
        {
            var auth = new GhostAPI(Url, ClientId, ClientSecret);

            var token = auth.GetAuthToken(ClientId, ClientSecret, UserName, Password);

            Assert.NotNull(token.AccessToken);
            Assert.NotEmpty(token.AccessToken);
            Assert.NotNull(token.RefreshToken);
            Assert.NotEmpty(token.RefreshToken);

            Assert.Equal(2628000, token.ExpiresIn);
            Assert.Equal("Bearer", token.TokenType);
        }

        [Fact]
        public void GetAuthToken_ThrowsException_WhenCredentialsInvalid()
        {
            var auth = new GhostAPI(Url, ClientId, ClientSecret);

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthToken("678", "90$!", "fake@fake.com", "12345"));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Access denied", ex.Errors[0].Message);
        }

        [Fact]
        public void GetPosts_ReturnsPosts_WhenClientIdAndSecretValid()
        {
            var auth = new GhostAPI(Url, ClientId, ClientSecret);

            var postResponse = auth.GetPosts(new PostQueryParams { Limit = 2, Fields = "id" });

            Assert.Equal(2, postResponse.Posts.Count);
        }

        [Fact]
        public void GetPosts_ReturnsPosts_WhenAuthTokenValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var postResponse = auth.GetPosts(new PostQueryParams { Limit = 2, Fields = "id" });

            Assert.Equal(2, postResponse.Posts.Count);
        }

        [Fact]
        public void GetPosts_ThrowsException_WhenAuthTokenInvalid()
        {
            var auth = new GhostAPI(Url, "invalid_token");

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPosts());
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Access denied", ex.Errors[0].Message);
        }

        [Fact]
        public void GetTags_ReturnsTags_WhenClientIdAndSecretValid()
        {
            var auth = new GhostAPI(Url, ClientId, ClientSecret);

            var tagResponse = auth.GetTags(new TagQueryParams { Limit = 2, Fields = "id" });

            Assert.Equal(2, tagResponse.Tags.Count);
        }

        [Fact]
        public void GetTags_ReturnsTags_WhenAuthTokenValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var tagResponse = auth.GetTags(new TagQueryParams { Limit = 2, Fields = "id" });

            Assert.Equal(2, tagResponse.Tags.Count);
        }

        [Fact]
        public void GetTags_ThrowsException_WhenAuthTokenInvalid()
        {
            var auth = new GhostAPI(Url, "invalid_token");

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTags());
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Access denied", ex.Errors[0].Message);
        }

        [Fact]
        public void GetUsers_ReturnsUsers_WhenClientIdAndSecretValid()
        {
            var auth = new GhostAPI(Url, ClientId, ClientSecret);

            var userResponse = auth.GetUsers(new UserQueryParams { Limit = 1, Fields = "id" });

            Assert.Equal(1, userResponse.Users.Count);
        }

        [Fact]
        public void GetUsers_ReturnsUsers_WhenAuthTokenValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var userResponse = auth.GetUsers(new UserQueryParams { Limit = 1, Fields = "id" });

            Assert.Equal(1, userResponse.Users.Count);
        }

        [Fact]
        public void GetUsers_ThrowsException_WhenAuthTokenInvalid()
        {
            var auth = new GhostAPI(Url, "invalid_token");

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetUsers());
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Access denied", ex.Errors[0].Message);
        }

        [Fact]
        public void GetMyProfile_ThrowsException_WhenCredentialsInvalid()
        {
            var auth = new GhostAPI(Url, "invalid_token");
        
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetMyProfile());
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Access denied", ex.Errors[0].Message);
        }
    }
}
