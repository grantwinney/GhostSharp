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
        public void GetPostById_ReturnsMatchingPost_WhenIdIsValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var post = auth.GetPostById(PostId);

            Assert.Equal(PostId, post.Id);
        }

        [Fact]
        public void GetPostById_ThrowsException_WhenIdIsInvalid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostById("invalid_id"));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Post not found", ex.Errors[0].Message);
        }

        [Fact]
        public void GetPostBySlug_ReturnsMatchingPost_WhenSlugIsValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var post = auth.GetPostBySlug(PostSlug);

            Assert.Equal(PostSlug, post.Slug);
        }

        [Fact]
        public void GetPostBySlug_ThrowsException_WhenSlugIsInvalid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostBySlug("invalid_slug"));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Post not found", ex.Errors[0].Message);
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
        public void GetTagById_ReturnsMatchingTag_WhenIdIsValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var tag = auth.GetTagById(TagId);

            Assert.Equal(TagId, tag.Id);
        }

        [Fact]
        public void GetTagById_ThrowsException_WhenIdIsInvalid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagById("invalid_id"));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Tag not found", ex.Errors[0].Message);
        }

        [Fact]
        public void GetTagBySlug_ReturnsMatchingTag_WhenSlugIsValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var tag = auth.GetTagBySlug(TagSlug);

            Assert.Equal(TagSlug, tag.Slug);
        }

        [Fact]
        public void GetTagBySlug_ThrowsException_WhenSlugIsInvalid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagBySlug("invalid_slug"));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Tag not found", ex.Errors[0].Message);
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
        public void GetUserById_ReturnsMatchingUser_WhenIdIsValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var user = auth.GetUserById(UserId);

            Assert.Equal(UserId, user.Id);
        }

        [Fact]
        public void GetUserById_ThrowsException_WhenIdIsInvalid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetUserById("invalid_id"));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("User not found", ex.Errors[0].Message);
        }

        [Fact]
        public void GetUserBySlug_ReturnsMatchingUser_WhenSlugIsValid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var user = auth.GetUserBySlug(UserSlug);

            Assert.Equal(UserSlug, user.Slug);
        }

        [Fact]
        public void GetUserBySlug_ThrowsException_WhenSlugIsInvalid()
        {
            var auth = new GhostAPI(Url, AuthToken);

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetUserBySlug("invalid_slug"));
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("User not found", ex.Errors[0].Message);
        }

        [Fact]
        public void IsPublicApiEnabled_ReturnsCorrectValue()
        {
            var auth = new GhostAPI(Url, ClientId, ClientSecret);

            // Try disabling the API on your site and change this to Assert.False
            Assert.True(auth.IsPublicApiEnabled());
        }
    }
}
