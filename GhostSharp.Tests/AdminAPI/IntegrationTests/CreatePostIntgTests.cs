using GhostSharp.Entities;
using GhostSharp.Enums;
using GhostSharp.QueryParams;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void CreatePost_ReturnsMatchingPost()
        {
            var expectedPost = new Post { Title = "This is a test post 1", MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"My post content. Work in progress...\"]]]]}", Status = "draft" };

            var posts = auth.CreatePost(new PostRequest { Posts = new List<Post> { expectedPost } });

            var actualPost = posts.Posts[0];

            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual(expectedPost.MobileDoc, actualPost.MobileDoc);
            Assert.AreEqual(expectedPost.Status, actualPost.Status);

            //Assert.AreEqual(ValidPost1Slug, post.Slug);
            //Assert.AreEqual(ValidPost1Title, post.Title);
            //Assert.AreEqual(ValidPost1Url, post.Url);

            //Assert.IsNotNull(post.Uuid);
            //Assert.IsNotNull(post.MobileDoc);
            //Assert.IsNotNull(post.CommentId);
            //Assert.IsNotNull(post.FeatureImage);
            //Assert.IsNotNull(post.MetaDescription);
            //Assert.IsNotNull(post.CreatedAt);
            //Assert.IsNotNull(post.UpdatedAt);
            //Assert.IsNotNull(post.PublishedAt);
            //Assert.IsNotNull(post.CustomExcerpt);
            //Assert.IsNotNull(post.OgDescription);
            //Assert.IsNotNull(post.TwitterDescription);
            //Assert.IsNotNull(post.Url);
            //Assert.IsNotNull(post.Excerpt);
            //Assert.IsNotNull(post.PrimaryAuthor);
            //Assert.IsNotNull(post.PrimaryTag);
            //Assert.IsNotNull(post.Authors);
            //Assert.AreEqual(1, post.Authors.Count);
            //Assert.IsNotNull(post.Tags);
            //Assert.AreEqual(3, post.Tags.Count);

            //Assert.IsNull(post.MetaTitle);
            //Assert.IsNull(post.CodeInjectionHead);
            //Assert.IsNull(post.CodeInjectionFoot);
            //Assert.IsNull(post.OgImage);
            //Assert.IsNull(post.OgTitle);
            //Assert.IsNull(post.TwitterImage);
            //Assert.IsNull(post.TwitterTitle);
            //Assert.IsNull(post.CustomTemplate);
            //Assert.IsNull(post.Html);
            //Assert.IsNull(post.PlainText);
        }

      
        //[TestCase(ExceptionLevel.Ghost)]
        //[TestCase(ExceptionLevel.All)]
        //public void GetPostBySlug_ThrowsGhostSharpException_WhenSlugIsInvalid(ExceptionLevel exceptionLevel)
        //{
        //    auth.ExceptionLevel = exceptionLevel;

        //    var ex = Assert.Throws<GhostSharpException>(() => auth.GetPostBySlug(InvalidPostSlug));

        //    Assert.IsNotEmpty(ex.Errors);
        //    Assert.AreEqual("Resource not found error, cannot read post.", ex.Errors[0].Message);
        //}

        //[TestCase(ExceptionLevel.None)]
        //[TestCase(ExceptionLevel.NonGhost)]
        //public void GetPostBySlug_ReturnsNull_WhenKeyIsInvalid_AndGhostExceptionsSuppressed(ExceptionLevel exceptionLevel)
        //{
        //    auth.ExceptionLevel = exceptionLevel;

        //    Assert.IsNull(auth.GetPostBySlug(InvalidPostSlug));
        //}
    }
}
