using System;
using GhostSharp;
using GhostSharp.Entities;
using Xunit;

namespace GhostSharpTests
{
    public class CreatePostTests : TestBase, IDisposable
    {
        readonly GhostAPI auth;
        Post createdPost;

        const string id = "yeah_it_wont_actually_use_this_id";
        const string title = "will this work???";
        const string slug = "some_really_long_slug_that_is_not_likely_to_be_used_2345";
        const string html = "I HOPE SO!";
        const string plainText = "I HOPE SO!";
        const string customExcerpt = "something short and sweet";

        public CreatePostTests()
        {
            createdPost = null;
            auth = new GhostAPI(Url, AuthToken);
        }

        [Fact]
        public void CreatePost_ReturnsPost_WhenPostCreated()
        {
            // todo: need to figure out why most fields aren't showing in post

            createdPost = auth.CreatePost(GeneratePost());

            Assert.Equal(title, createdPost.Title);
            Assert.Equal(slug, createdPost.Slug);
            //Assert.Equal(html, createdPost.Html);
            //Assert.Equal(plainText, createdPost.PlainText);
            //Assert.Equal(customExcerpt, createdPost.CustomExcerpt);
            Assert.Equal("draft", createdPost.Status);
        }

        [Fact]
        public void CreatePost_IgnoresUserSpecifiedId()
        {
            createdPost = auth.CreatePost(GeneratePost());

            Assert.NotEqual(id, createdPost.Id);
        }

        // This passes when it should not, because most fields aren't being carried over in a POST.
        //[Fact]
        public void CreatePost_ThrowsException_WhenSuppressionLevelNone()
        {
            var ex = Assert.Throws<GhostSharpException>(() => createdPost = auth.CreatePost(new Post { CreatedAt = "invalid_creation_time" }));

            Assert.NotEmpty(ex.Errors);
            Assert.Equal("", ex.Errors[0].ErrorType);
            Assert.Equal("", ex.Errors[0].Message);
        }

        // These pass when they should not, because most fields aren't being carried over in a POST.
        //[Theory]
        //[InlineData(SuppressionLevel.GhostOnly)]
        //[InlineData(SuppressionLevel.All)]
        public void CreatePost_ReturnsNull_WhenSuppressionLevelNotNone(SuppressionLevel level)
        {
            auth.SuppressionLevel = level;

            createdPost = auth.CreatePost(new Post { CreatedAt = "invalid_creation_time" });
           
            Assert.Null(createdPost);
            Assert.NotNull(auth.LastException);
        }

        public void Dispose()
        {
            if (createdPost != null)
                auth.DeletePostById(createdPost.Id);
            createdPost = null;
        }

        Post GeneratePost() => new Post
        {
            Title = title,
            Slug = slug,
            Html = html,
            PlainText = plainText,
            CustomExcerpt = customExcerpt
        };
    }
}
