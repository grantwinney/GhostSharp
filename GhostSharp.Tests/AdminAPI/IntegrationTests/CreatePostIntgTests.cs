using GhostSharp.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class CreatePostIntgTests : TestBase
    {
        private const int MINIMUM_POST_COUNT_THRESHHOLD = 200;

        private GhostAdminAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        string newPostId = null;

        [TearDown]
        public void TearDown()
        {
            auth.DeletePost(newPostId);

            newPostId = null;
        }

        [Test]
        public void CreatePost_CreatesBasicPost()
        {
            var expectedPost = new Post
            {
                Title = "This is a test post 1",
                MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"My post content. Work in progress...\"]]]]}",
                Status = "draft"
            };

            var posts = auth.CreatePost(new PostRequest { Posts = new List<Post> { expectedPost } });

            var actualPost = posts.Posts[0];

            newPostId = actualPost.Id;

            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual(expectedPost.MobileDoc, actualPost.MobileDoc);
            Assert.AreEqual(expectedPost.Status, actualPost.Status);
            Assert.IsNotNull(actualPost.Title.Replace(' ', '-').ToLower(), actualPost.Slug);

            Assert.IsNotNull(actualPost.Uuid);
            Assert.IsTrue(Guid.TryParse(actualPost.Uuid, out Guid ignoreResult));
            Assert.IsNotNull(actualPost.CommentId);
            Assert.IsNotNull(actualPost.CreatedAt);
            Assert.IsNotNull(actualPost.UpdatedAt);
            Assert.IsNotNull(actualPost.Url);
            Assert.AreEqual($"{Host}/404/", actualPost.Url);
            Assert.IsNotNull(actualPost.Excerpt);
            Assert.AreEqual("My post content. Work in progress...", actualPost.Excerpt);
            Assert.IsNotNull(actualPost.PrimaryAuthor);
            Assert.IsNotNull(actualPost.Authors);
            Assert.AreEqual(1, actualPost.Authors.Count);

            Assert.IsNull(actualPost.MetaTitle);
            Assert.IsNull(actualPost.MetaDescription);
            Assert.IsNull(actualPost.CodeInjectionHead);
            Assert.IsNull(actualPost.CodeInjectionFoot);
            Assert.IsNull(actualPost.OgImage);
            Assert.IsNull(actualPost.OgTitle);
            Assert.IsNull(actualPost.TwitterImage);
            Assert.IsNull(actualPost.TwitterTitle);
            Assert.IsNull(actualPost.CustomTemplate);
            Assert.IsNull(actualPost.Html);
            Assert.IsNull(actualPost.PublishedAt);
            Assert.IsNull(actualPost.PlainText);
            Assert.IsNull(actualPost.FeatureImage);
            Assert.IsNull(actualPost.CustomExcerpt);
            Assert.IsNull(actualPost.OgDescription);
            Assert.IsNull(actualPost.TwitterDescription);
            Assert.IsNull(actualPost.CanonicalUrl);
            Assert.IsNull(actualPost.PrimaryTag);

            Assert.IsEmpty(actualPost.Tags);
            Assert.IsFalse(actualPost.Featured);
        }

        // make sure default author is assigned

        // make sure missing status defaults to draft
    }
}
