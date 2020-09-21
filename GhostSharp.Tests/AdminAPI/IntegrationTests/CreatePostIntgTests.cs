using GhostSharp.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class CreatePostIntgTests : TestBase
    {
        private GhostAdminAPI auth;
        private string newPostId = null;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [TearDown]
        public void TearDown()
        {
            if (newPostId != null)
                auth.DeletePost(newPostId);

            newPostId = null;
        }

        [Test]
        public void CreatePost_CreatesBasicPost()
        {
            var expectedPost = new Post
            {
                Title = "This is a test post",
                MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"My post content. Work in progress...\"]]]]}",
                Status = "draft"
            };

            var actualPost = auth.CreatePost(expectedPost);

            newPostId = actualPost.Id;

            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual(expectedPost.MobileDoc, actualPost.MobileDoc);
            Assert.AreEqual(expectedPost.Status, actualPost.Status);
            Assert.AreEqual(actualPost.Title.Replace(' ', '-').ToLower(), actualPost.Slug);

            Assert.IsNotNull(actualPost.Uuid);
            Assert.IsTrue(Guid.TryParse(actualPost.Uuid, out _));
            Assert.IsNotNull(actualPost.CommentId);
            Assert.IsNotNull(actualPost.CreatedAt);
            Assert.IsNotNull(actualPost.UpdatedAt);
            Assert.IsNotNull(actualPost.Url);
            Assert.IsTrue(actualPost.Url.StartsWith($"{Host}p/"));
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

        [Test]
        public void CreatePost_SetsMissingStatusToDraft()
        {
            var expectedPost = new Post
            {
                Title = "This is a test post with missing status"
            };

            var actualPost = auth.CreatePost(expectedPost);

            newPostId = actualPost.Id;

            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual("draft", actualPost.Status);
        }

        [Test]
        public void CreatePost_SetsMissingAuthorToOwner()
        {
            var expectedPost = new Post
            {
                Title = "This is a test post with missing author",
                Status = "draft"
            };

            var actualPost = auth.CreatePost(expectedPost);

            newPostId = actualPost.Id;

            Assert.AreEqual(expectedPost.Title, actualPost.Title);

            Assert.IsNotNull(actualPost.PrimaryAuthor);
            Assert.AreEqual(ValidAuthor1Id, actualPost.PrimaryAuthor.Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPost.PrimaryAuthor.Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPost.PrimaryAuthor.Name);
            Assert.AreEqual(ValidAuthor1Url, actualPost.PrimaryAuthor.Url);

            Assert.IsNotNull(actualPost.Authors);
            Assert.AreEqual(1, actualPost.Authors.Count);
            Assert.AreEqual(ValidAuthor1Id, actualPost.Authors[0].Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPost.Authors[0].Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPost.Authors[0].Name);
            Assert.AreEqual(ValidAuthor1Url, actualPost.Authors[0].Url);
        }

        [Test]
        public void CreatePost_CanUseHtmlInsteadOfMobileDoc()
        {
            var expectedPost = new Post
            {
                Title = "This is a test post using html instead of mobiledoc",
                Html = "<h1>Test Header</h1><b>Test Body</b>",
                Status = "draft"
            };

            var actualPost = auth.CreatePost(expectedPost);

            newPostId = actualPost.Id;

            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual(expectedPost.Status, actualPost.Status);
            Assert.AreEqual(actualPost.Title.Replace(' ', '-').ToLower(), actualPost.Slug);
            Assert.AreEqual("Test Header\nTest Body", actualPost.Excerpt);

            Assert.AreEqual(
                "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[[\"b\"]],\"sections\":[[1,\"h1\",[[0,[],0,\"Test Header\"]]],[1,\"p\",[[0,[0],1,\"Test Body\"]]]]}",
                actualPost.MobileDoc);

            Assert.IsNull(actualPost.Html);
            Assert.IsNull(actualPost.PlainText);
        }

        [Test]
        public void CreatePost_RequiresOnlyATitle()
        {
            var expectedPost = new Post
            {
                Title = "This is a test post with nothing but title"
            };

            var actualPost = auth.CreatePost(expectedPost);

            newPostId = actualPost.Id;

            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual("draft", actualPost.Status);
            Assert.AreEqual(actualPost.Title.Replace(' ', '-').ToLower(), actualPost.Slug);

            Assert.IsNull(actualPost.Html);
            Assert.IsNotNull(actualPost.MobileDoc);
            Assert.IsNull(actualPost.PlainText);
        }

        [Test]
        public void CreatePost_RequiresAtLeastATitle()
        {
            var exception = Assert.Throws<GhostSharpException>(() => auth.CreatePost(new Post { Title = null }));

            Assert.AreEqual("Validation error, cannot save post.", exception.Message);
        }

        [Test]
        public void CreatePost_CanCreatePost_WithHtmlBody()
        {
            var expectedPost = new Post
            {
                Title = "This is a test post with an html body",
                Html = "<b>This is an html body!!</b>",
                Status = "draft"
            };

            var actualPost = auth.CreatePost(expectedPost);

            newPostId = actualPost.Id;

            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual(
                "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[[\"b\"]],\"sections\":[[1,\"p\",[[0,[0],1,\"This is an html body!!\"]]]]}",
                actualPost.MobileDoc);
            Assert.IsNull(actualPost.Html);
        }

        [Test]
        public void CreatePost_CanAttachTagsUsingLongForm_WhenUsingTagsField()
        {
            var expectedPost = new Post
            {
                Title = "This is a test post with long form tags",
                Status = "draft",
                Tags = new List<Tag>
                {
                    new Tag { Name = ValidTag1Name },
                    new Tag { Name = ValidTag2Name }
                }
            };

            var actualPost = auth.CreatePost(expectedPost);

            newPostId = actualPost.Id;

            Assert.AreEqual(expectedPost.Title, actualPost.Title);

            Assert.AreEqual(ValidTag1Id, actualPost.PrimaryTag.Id);
            Assert.AreEqual(ValidTag1Slug, actualPost.PrimaryTag.Slug);
            Assert.AreEqual(ValidTag1Name, actualPost.PrimaryTag.Name);

            Assert.AreEqual(ValidTag1Id, actualPost.Tags[0].Id);
            Assert.AreEqual(ValidTag1Slug, actualPost.Tags[0].Slug);
            Assert.AreEqual(ValidTag1Name, actualPost.Tags[0].Name);

            Assert.AreEqual(ValidTag2Id, actualPost.Tags[1].Id);
            Assert.AreEqual(ValidTag2Slug, actualPost.Tags[1].Slug);
            Assert.AreEqual(ValidTag2Name, actualPost.Tags[1].Name);
        }

        [Test]
        public void CreatePost_CanAttachAuthorsUsingLongForm_WhenUsingAuthorsField()
        {
            var expectedPost = new Post
            {
                Title = "This is a test post with long form authors",
                Status = "draft",
                Authors = new List<Author>
                {
                    new Author { Id = ValidAuthor1Id },
                    new Author { Slug = ValidAuthor2Slug }
                }
            };

            var actualPost = auth.CreatePost(expectedPost);

            newPostId = actualPost.Id;

            Assert.AreEqual(expectedPost.Title, actualPost.Title);

            Assert.AreEqual(ValidAuthor1Id, actualPost.PrimaryAuthor.Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPost.PrimaryAuthor.Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPost.PrimaryAuthor.Name);

            Assert.AreEqual(ValidAuthor1Id, actualPost.Authors[0].Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPost.Authors[0].Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPost.Authors[0].Name);

            Assert.AreEqual(ValidAuthor2Id, actualPost.Authors[1].Id);
            Assert.AreEqual(ValidAuthor2Slug, actualPost.Authors[1].Slug);
            Assert.AreEqual(ValidAuthor2Name, actualPost.Authors[1].Name);
        }

        [Test]
        public void CreatePost_CanNotAttachTagUsingLongForm_WhenUsingPrimaryTag()
        {
            var actualPost = auth.CreatePost(
                new Post
                {
                    Title = "This is a test post with long form tag by id",
                    PrimaryTag = new Tag { Id = ValidTag1Id },
                    Status = "draft"
                });

            newPostId = actualPost.Id;

            Assert.IsEmpty(actualPost.Tags);
            Assert.IsNull(actualPost.PrimaryTag);
        }

        [Test]
        public void CreatePost_CanNotAttachAuthorUsingLongForm_WhenUsingPrimaryAuthor()
        {
            var actualPost = auth.CreatePost(
                new Post
                {
                    Title = "This is a test post with long form author using id",
                    PrimaryAuthor = new Author { Id = ValidAuthor2Id },
                    Status = "draft"
                });

            newPostId = actualPost.Id;

            // It attaches the default owner, not the alternate author I specified above
            Assert.AreEqual(ValidAuthor1Id, actualPost.PrimaryAuthor.Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPost.PrimaryAuthor.Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPost.PrimaryAuthor.Name);

            Assert.AreEqual(ValidAuthor1Id, actualPost.Authors[0].Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPost.Authors[0].Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPost.Authors[0].Name);
        }

        [Test]
        public void CreatePost_AttachesOwnerRoleAsAuthor_WhenSpecifiedAuthorNotFound()
        {
            var expectedPost = new Post
            {
                Title = "This is a test post with invalid author",
                PrimaryAuthor = new Author { Id = InvalidAuthorId }
            };

            var actualPost = auth.CreatePost(expectedPost);

            newPostId = actualPost.Id;

            Assert.AreEqual(expectedPost.Title, actualPost.Title);
            Assert.AreEqual("draft", actualPost.Status);

            Assert.AreEqual(ValidAuthor1Id, actualPost.PrimaryAuthor.Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPost.PrimaryAuthor.Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPost.PrimaryAuthor.Name);

            Assert.AreEqual(ValidAuthor1Id, actualPost.Authors[0].Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPost.Authors[0].Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPost.Authors[0].Name);

            Assert.AreEqual(1, actualPost.Authors.Count);
        }
    }
}
