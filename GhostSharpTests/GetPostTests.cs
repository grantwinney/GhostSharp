using System;
using GhostSharp;
using GhostSharp.Entities;
using GhostSharp.QueryParams;
using Xunit;

namespace GhostSharpTests
{
    public class GetPostTests : TestBase, IDisposable
    {
        readonly GhostAPI auth;
        Post createdPost;

        const string unlikelyTitle = "some-really-random-title-that-i-hope-no-one-uses-234566";

        public GetPostTests()
        {
            createdPost = null;
            auth = new GhostAPI(Url, AuthToken);
        }

        [Fact]
        public void GetPostById_ReturnsMatchingPost_WhenIdIsValid()
        {
            createdPost = auth.CreatePost(new Post { Title = unlikelyTitle });

            var actualPostId = auth.GetPostById(createdPost.Id, new PostQueryParams { Status = "draft" }).Id;

            Assert.Equal(createdPost.Id, actualPostId);
        }

        [Fact]
        public void GetPostById_ThrowsException_WhenIdIsInvalid()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostById("invalid_id"));
        
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Post not found", ex.Errors[0].Message);
        }

        [Fact]
        public void GetPostBySlug_ReturnsMatchingPost_WhenSlugIsValid()
        {
            createdPost = auth.CreatePost(new Post { Title = unlikelyTitle, Slug = unlikelyTitle });

            Assert.Equal(createdPost.Slug, auth.GetPostBySlug(createdPost.Slug, new PostQueryParams { Status = "draft" }).Slug);
        }

        [Fact]
        public void GetPostBySlug_ThrowsException_WhenSlugIsInvalid()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostBySlug("invalid_slug"));
        
            Assert.NotEmpty(ex.Errors);
            Assert.StartsWith("Post not found", ex.Errors[0].Message);
        }

        public void Dispose()
        {
            if (createdPost != null)
                auth.DeletePostById(createdPost.Id);
            createdPost = null;
        }
    }
}
