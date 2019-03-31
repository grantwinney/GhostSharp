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

        [TestCase(2)]
        [TestCase(-2)]
        [Test]
        public void UpdatePost_Succeeds_WhenUpdatedAtIsDifferent_BecauseItsForcedToBeSameAsOriginalPost(int minutes)
        {
            var updatedPost = new Post
            {
                Id = origPost.Id,
                Title = "a different title...",
                UpdatedAt = origPost.UpdatedAt.Value.AddMinutes(minutes)
            };

            Assert.DoesNotThrow(() => auth.UpdatePost(updatedPost));
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

            Assert.AreEqual("Resource not found error, cannot read post.", ex.Message);
        }

        [Test]
        public void UpdatePost_Succeeds_WhenAllFieldsChange_AsLongAsOnlyUpdatableFieldsAreSent()
        {
            Post origPost = null;
            try
            {
                origPost = auth.CreatePost(
                    new Post
                    {
                        Page = true,
                        Title = "Original Title",
                        Slug = "original-improbable-sluggy-slug-name-sluggish-1234235",
                        MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Original Post Content\"]]]]}",
                        Html = "<h1>Original</h1><p>Html</p>",
                        PlainText = "original plaintext",
                        FeatureImage = "original_image.jpg",
                        Featured = true,
                        MetaTitle = "Original Meta Title",
                        MetaDescription = "Original Meta Description",
                        PublishedAt = DateTime.Now.AddMinutes(10),
                        CustomExcerpt = "Original Excerpt",
                        CodeInjectionHead = "Original Header",
                        CodeInjectionFoot = "Original Footer",
                        OgImage = "original_og_image",
                        OgTitle = "Original OG Title",
                        OgDescription = "Original OG Description",
                        TwitterImage = "original_twitter_image",
                        TwitterTitle = "Original Twitter Title",
                        TwitterDescription = "Original Twitter Description",
                        CustomTemplate = "Original Custom Template",
                        Url = "original_url",
                        CanonicalUrl = "original_canonical_url",
                        Excerpt = "Original Excerpt",
                        Status = "draft",
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
                        Featured = false,
                        MetaTitle = "Updated Meta Title",
                        MetaDescription = "Updated Meta Description",
                        PublishedAt = DateTime.Now.AddMinutes(30),
                        CustomExcerpt = "Updated Excerpt",
                        CodeInjectionHead = "Updated Header",
                        CodeInjectionFoot = "Updated Footer",
                        OgImage = "updated_og_image",
                        OgTitle = "Updated OG Title",
                        OgDescription = "Updated OG Description",
                        TwitterImage = "updated_twitter_image",
                        TwitterTitle = "Updated Twitter Title",
                        TwitterDescription = "Updated Twitter Description",
                        CustomTemplate = "Updated Custom Template",
                        CanonicalUrl = "updated_canonical_url",
                        Excerpt = "Updated Excerpt",
                        Status = "draft",

                        // cannot be changed
                        Slug = "updated-improbable-sluggy-slug-name-sluggish-1234235",
                        Uuid = "FA6668B8-059B-495E-AD4C-7F4569AE2C26",
                        CommentId = "5c9eccccccafbecccccccccc",

                        // ignored either way
                        Page = false,
                        CreatedAt = origPost.CreatedAt.Value.AddSeconds(2),
                        Url = "updated_url",
                    });

                Assert.AreEqual(origPost.Id, updatedPost.Id);
                Assert.AreNotEqual(origPost.Title, updatedPost.Title);
            }
            finally
            {
                if (origPost != null)
                    auth.DeletePost(origPost.Id);
            }
        }
    }

    // finish asserts in above test

    // is title ALWAYS required (I think it can't be left blank)?? are all writable fields required even if not changing?? :/

    // do a GET and save it right back in?

    // Tag and author relations will be replaced, not merged - test this

    // which takes precedence if html AND mobiledoc are both updated?
    //   need to attach query parameter to specify html, like when creating post?

    // should i create a CreatableAttribute too to ignore fields that shouldn't be sent,
    //   like id or uuid?

    // then copy all of this for pages

}
