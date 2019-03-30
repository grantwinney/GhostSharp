using GhostSharp.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class UpdatePostIntgTests : TestBase
    {
        private GhostAdminAPI auth;
        private Post origPost = null;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);

            origPost = auth.CreatePost(new Post { Title = "Sample post used for update post tests", MetaTitle = "sample meta title", Page = true });
        }

        [TearDown]
        public void TearDown()
        {
            if (origPost != null)
                auth.DeletePost(origPost.Id);

            origPost = null;
        }

        [Test]
        public void UpdatePost_Fails_WhenUpdatedAtMissing()
        {
            var updatedPost = new Post
            {
                Id = origPost.Id
            };

            var ex = Assert.Throws<GhostSharpException>(() => auth.UpdatePost(updatedPost));

            Assert.AreEqual("Validation error, cannot edit post.", ex.Message);
        }

        [TestCase(2)]
        [TestCase(-2)]
        [Test]
        public void UpdatePost_Fails_WhenUpdatedAtIsDifferent_AndAnUpdatableFieldChanges(int minutes)
        {
            var updatedPost = new Post
            {
                Id = origPost.Id,
                Title = "a different title...",
                UpdatedAt = origPost.UpdatedAt.Value.AddMinutes(minutes)
            };

            var ex = Assert.Throws<GhostSharpException>(() => auth.UpdatePost(updatedPost));

            Assert.AreEqual("Saving failed! Someone else is editing this post.", ex.Message);
        }

        [Test]
        public void UpdatePost_Succeeds_WhenUpdatedAtIsDifferent_ButNoUpdatableFieldsChange()
        {
            var updatedPost = auth.UpdatePost(
                new Post
                {
                    Id = origPost.Id,
                    Title = origPost.Title,
                    UpdatedAt = origPost.UpdatedAt.Value.AddMinutes(2)
                });

            Assert.AreEqual(origPost.Id, updatedPost.Id);
            Assert.AreEqual(origPost.Title, updatedPost.Title);
            Assert.AreEqual(origPost.UpdatedAt.Value.AddMinutes(2), updatedPost.UpdatedAt);
        }

        [Test]
        public void UpdatePost_Succeeds_WhenUpdatedAtIsSame_AndUpdatableFieldsChange()
        {
            var updatedPost = auth.UpdatePost(
                new Post
                {
                    Id = origPost.Id,
                    Title = "a different title...",
                    UpdatedAt = origPost.UpdatedAt
                });

            Assert.AreEqual(origPost.Id, updatedPost.Id);
            Assert.AreEqual("a different title...", updatedPost.Title);
            Assert.GreaterOrEqual(updatedPost.UpdatedAt, origPost.UpdatedAt);
        }

        [Test]
        public void UpdatePost_Fails_WhenIdIsOmitted()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.UpdatePost(
                new Post
                {
                    UpdatedAt = origPost.UpdatedAt
                }));

            Assert.AreEqual("Resource not found", ex.Message);
        }

        [Test]
        public void UpdatePost_Fails_WhenIdIsInvalid()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.UpdatePost(
                new Post
                {
                    Id = InvalidPostId,
                    UpdatedAt = origPost.UpdatedAt,
                    Title = origPost.Title
                }));

            Assert.AreEqual("Resource not found error, cannot edit post.", ex.Message);
        }

        [Test]
        public void UpdatePost_Succeeds_WhenAllFieldsChange_AsLongAsOnlyUpdatableFieldsAreSent()
        {
            var origPost = auth.CreatePost(
                new Post
                {
                    Page = true,
                    Title = "Original Title",
                    Slug = "original-improbable-sluggy-slug-name-sluggish-1234235",
                    MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Original Post Content\"]]]]}",
                    Html = "<h1>Original</h1><p>Html</p>",
                    PlainText = "original plaintext",
                    FeatureImage = "original_image.jpg",
                    MetaTitle = "Original Meta Title",
                });

            var updatedPost = auth.UpdatePost(
                new Post
                {
                    // must be provided, remain same
                    Id = origPost.Id,
                    UpdatedAt = origPost.UpdatedAt,

                    // can be changed
                    Title = "Updated Title",
                    MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Updated Post Content\"]]]]}",
                    Html = "<h1>Updated</h1><p>Html</p>",
                    PlainText = "updated plaintext",
                    FeatureImage = "updated_image.jpg",

                    // cannot be changed
                    Slug = "updated-improbable-sluggy-slug-name-sluggish-1234235",
                    Uuid = "FA6668B8-059B-495E-AD4C-7F4569AE2C26",
                    CommentId = "5c9eccccccafbecccccccccc",

                    // ignored either way
                    Page = false,

                    MetaTitle = "Updated Meta Title",
                });

            Assert.AreEqual(origPost.Id, updatedPost.Id);
            Assert.AreNotEqual(origPost.Title, updatedPost.Title);
        }
    }

    // is title ALWAYS required (I think it can't be left blank)?? are all writable fields required even if not changing?? :/

    // what happens with slugs with a space in them?

    // update a single post

    // do a GET and save it right back in?

    // Tag and author relations will be replaced, not merged - test this

    // then copy all of this for pages

    // then implement site - easy - NO AUTH!

    // which takes precedence if html AND mobiledoc are both updated?
    //   need to attach query parameter to specify html, like when creating post?

    // should i create a CreatableAttribute too to ignore fields that shouldn't be sent,
    //   like id or uuid?

}
