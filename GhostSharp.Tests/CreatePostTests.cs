using System;
using GhostSharp;
using GhostSharp.Entities;
using NUnit;
using NUnit.Framework;

namespace GhostSharpTests
{
    [TestFixture]
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

        [Test]
        public void CreatePost_ReturnsPost_WhenPostCreated()
        {
            createdPost = auth.CreatePost(GeneratePost());

            Assert.AreEqual(title, createdPost.Title);
            Assert.AreEqual(slug, createdPost.Slug);
            //Assert.Equal(html, createdPost.Html);
            //Assert.Equal(plainText, createdPost.PlainText);
            //Assert.Equal(customExcerpt, createdPost.CustomExcerpt);
            Assert.AreEqual("draft", createdPost.Status);
        }

        [Test]
        public void CreatePost_IgnoresUserSpecifiedId()
        {
            createdPost = auth.CreatePost(GeneratePost());

            Assert.AreNotEqual(id, createdPost.Id);
        }

        // This passes when it should not, because most fields aren't being carried over in a POST.
        //[Fact]
        public void CreatePost_ThrowsException_WhenSuppressionLevelNone()
        {
            var ex = Assert.Throws<GhostSharpException>(() => createdPost = auth.CreatePost(new Post { CreatedAt = "invalid_creation_time" }));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("", ex.Errors[0].ErrorType);
            Assert.AreEqual("", ex.Errors[0].Message);
        }

        // These pass when they should not, because most fields aren't being carried over in a POST.
        //[TestCase(SuppressionLevel.GhostOnly)]
        //[TestCase(SuppressionLevel.All)]
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
