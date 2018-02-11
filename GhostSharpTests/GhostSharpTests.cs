using GhostSharp;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using Xunit;

namespace GhostSharpTests
{
    public partial class GhostSharpTests
    {
        [Fact]
        public void GetAuthToken_ReturnsToken_WhenCredentialsValid()
        {
            var auth = new GhostAPI(Url, ClientId, ClientSecret);

            AuthToken token = auth.GetAuthToken(UserName, Password, ClientId, ClientSecret);

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

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetAuthToken("fake@fake.com", "12345", "678", "90$!"));
            Assert.NotEmpty(ex.Errors);
            Assert.Equal("Access denied.", ex.Errors[0].Message);
        }

        [Fact]
        public void GetPosts_ReturnsPosts_WhenClientIdAndSecretValid()
        {
            var auth = new GhostAPI(Url, ClientId, ClientSecret);

            var posts = auth.GetPosts(new PostQueryParams { Limit = 2, Fields = "id" });

            Assert.Equal(2, posts.Count);
        }

        [Fact]
        public void GetPosts_ReturnsPosts_WhenAuthTokenValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var posts = auth.GetPosts(new PostQueryParams { Limit = 2, Fields = "id" });

            Assert.Equal(2, posts.Count);
        }

        [Fact]
        public void GetPost_ReturnsMatchingPost_WhenIdIsValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var post = auth.GetPostById(PostId);

            Assert.Equal(PostId, post.Id);
        }

        [Fact]
        public void GetPost_ReturnsWhat_WhenIdIsInvalid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostById("invalid_id"));
            Assert.NotEmpty(ex.Errors);
            Assert.Equal("Post not found.", ex.Errors[0].Message);
        }


        [Fact]
        public void GetPost_ReturnsMatchingPost_WhenSlugIsValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var post = auth.GetPostBySlug(PostSlug);

            Assert.Equal(PostSlug, post.Slug);
        }

        [Fact]
        public void GetPost_ReturnsWhat_WhenSlugIsInvalid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostBySlug("invalid_slug"));
            Assert.NotEmpty(ex.Errors);
            Assert.Equal("Post not found.", ex.Errors[0].Message);
        }

        [Fact]
        public void IsPublicApiEnabled_ReturnsCorrectValue()
        {
            var auth = new GhostAPI(Url, ClientId, ClientSecret);

            Assert.True(auth.IsPublicApiEnabled());
        }
    }
}
