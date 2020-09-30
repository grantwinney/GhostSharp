using GhostSharp.Entities;
using GhostSharp.Tests.TestHelpers;
using NUnit.Framework;
using System;

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

            origPost = auth.CreatePost(
                new Post
                {
                    Title = "Sample post used for update post tests",
                    MetaTitle = "sample meta title",
                });
        }

        [TearDown]
        public void TearDown()
        {
            if (origPost != null)
                auth.DeletePost(origPost.Id);

            origPost = null;
        }

        [Test]
        public void UpdatePost_Succeeds_WhenAllFieldsChange()
        {
            Post origPost = null;

            var originalTitle = "Original Title";
            var originalSlug = "original-improbable-sluggy-slug-name-sluggish-1234235";
            var originalMobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Original Post Content\"]]]]}";
            var originalHtml = "<h1>Original</h1><p>Html</p>";
            var originalFeatureImage = "original_image.jpg";
            var originalMetaTitle = "Original Meta Title";
            var originalMetaDescription = "Original Meta Description";
            var originalPublishedAt = DateTime.UtcNow;
            var originalCustomExcerpt = "Original Excerpt";
            var originalCodeInjectionHead = "Original Header";
            var originalCodeInjectionFoot = "Original Footer";
            var originalOgImage = "original_og_image";
            var originalOgTitle = "Original OG Title";
            var originalOgDescription = "Original OG Description";
            var originalTwitterImage = "original_twitter_image";
            var originalTwitterTitle = "Original Twitter Title";
            var originalTwitterDescription = "Original Twitter Description";
            var originalCustomTemplate = "Original Custom Template";
            var originalCanonicalUrl = "original_canonical_url";
            var originalExcerpt = "Original Excerpt";
            var originalStatus = "draft";
            var originalVisibility = "public";
            var originalEmailSubject = "original email subject";

            try
            {
                origPost = auth.CreatePost(
                    new Post
                    {
                        Title = originalTitle,
                        Slug = originalSlug,
                        MobileDoc = originalMobileDoc,
                        Html = originalHtml,
                        FeatureImage = originalFeatureImage,
                        Featured = true,
                        MetaTitle = originalMetaTitle,
                        MetaDescription = originalMetaDescription,
                        PublishedAt = originalPublishedAt,
                        CustomExcerpt = originalCustomExcerpt,
                        CodeInjectionHead = originalCodeInjectionHead,
                        CodeInjectionFoot = originalCodeInjectionFoot,
                        OgImage = originalOgImage,
                        OgTitle = originalOgTitle,
                        OgDescription = originalOgDescription,
                        TwitterImage = originalTwitterImage,
                        TwitterTitle = originalTwitterTitle,
                        TwitterDescription = originalTwitterDescription,
                        CustomTemplate = originalCustomTemplate,
                        Excerpt = originalExcerpt,
                        CanonicalUrl = originalCanonicalUrl,
                        Status = originalStatus,
                        Visibility = originalVisibility,
                        EmailSubject = originalEmailSubject,
                    });

                origPost.Title = "Updated Title";
                origPost.Slug = "updated-improbable-sluggy-slug-name-sluggish-1234235";
                origPost.MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Updated Post Content\"]]]]}";
                origPost.Html = "<h1>Updated</h1><p>Html</p>";
                origPost.FeatureImage = "updated_image.jpg";
                origPost.Featured = false;
                origPost.MetaTitle = "Updated Meta Title";
                origPost.MetaDescription = "Updated Meta Description";
                origPost.PublishedAt = originalPublishedAt.AddMinutes(20);
                origPost.CustomExcerpt = "Updated Excerpt";
                origPost.CodeInjectionHead = "Updated Header";
                origPost.CodeInjectionFoot = "Updated Footer";
                origPost.OgImage = "updated_og_image";
                origPost.OgTitle = "Updated OG Title";
                origPost.OgDescription = "Updated OG Description";
                origPost.TwitterImage = "updated_twitter_image";
                origPost.TwitterTitle = "Updated Twitter Title";
                origPost.TwitterDescription = "Updated Twitter Description";
                origPost.CustomTemplate = "Updated Custom Template";
                origPost.Excerpt = "Updated Excerpt";
                origPost.CanonicalUrl = "updated_canonical_url";
                origPost.Status = "scheduled";
                origPost.Visibility = "paid";
                origPost.EmailSubject = "updated email subject";

                var updatedPost = auth.UpdatePost(origPost);

                Assert.AreNotEqual(originalTitle, updatedPost.Title);
                Assert.AreNotEqual(originalSlug, updatedPost.Slug);
                Assert.AreNotEqual(originalMobileDoc, updatedPost.MobileDoc);
                Assert.AreNotEqual(originalHtml, updatedPost.Html);
                Assert.AreNotEqual(originalFeatureImage, updatedPost.FeatureImage);
                Assert.IsFalse(updatedPost.Featured);
                Assert.AreNotEqual(originalMetaTitle, updatedPost.MetaTitle);
                Assert.AreNotEqual(originalMetaDescription, updatedPost.MetaDescription);
                Assert.AreEqual(originalPublishedAt.AddMinutes(20).RemoveTicks(), updatedPost.PublishedAt);
                Assert.AreNotEqual(originalCustomExcerpt, updatedPost.CustomExcerpt);
                Assert.AreNotEqual(originalCodeInjectionHead, updatedPost.CodeInjectionHead);
                Assert.AreNotEqual(originalCodeInjectionFoot, updatedPost.CodeInjectionFoot);
                Assert.AreNotEqual(originalOgImage, updatedPost.OgImage);
                Assert.AreNotEqual(originalOgTitle, updatedPost.OgTitle);
                Assert.AreNotEqual(originalOgDescription, updatedPost.OgDescription);
                Assert.AreNotEqual(originalTwitterImage, updatedPost.TwitterImage);
                Assert.AreNotEqual(originalTwitterTitle, updatedPost.TwitterTitle);
                Assert.AreNotEqual(originalTwitterDescription, updatedPost.TwitterDescription);
                Assert.AreNotEqual(originalCustomTemplate, updatedPost.CustomTemplate);
                Assert.AreNotEqual(originalExcerpt, updatedPost.Excerpt);
                Assert.AreNotEqual(originalCanonicalUrl, updatedPost.CanonicalUrl);
                Assert.AreNotEqual(originalStatus, updatedPost.Status);
                Assert.AreNotEqual(originalVisibility, updatedPost.Visibility);
                Assert.AreNotEqual(originalEmailSubject, updatedPost.EmailSubject);
            }
            finally
            {
                if (origPost != null)
                    auth.DeletePost(origPost.Id);
            }
        }

        //[Test]
        //public void SendEmailWhenPublished_IsSentAsQueryParameter_ButStillDoesntSeemToWork()
        //{
        //    origPost.Status = "scheduled";
        //    origPost.PublishedAt = DateTime.UtcNow.AddYears(300);
        //    //origPost.SendEmailWhenPublished = true;

        //    var updatedPost = auth.UpdatePost(origPost);

        //    Assert.AreEqual(origPost.Id, updatedPost.Id);
        //    Assert.False(origPost.SendEmailWhenPublished);
        //    Assert.False(updatedPost.SendEmailWhenPublished);  // SHOULD BE TRUE
        //}

        [Test]
        public void UpdatePost_DoesNotChangeSlugOrURL_WhenTitleIsModified()
        {
            var originalTitle = origPost.Title;

            origPost.Title = "a different title...";

            var updatedPost = auth.UpdatePost(origPost);

            Assert.AreEqual(origPost.Id, updatedPost.Id);
            Assert.AreEqual(origPost.Slug, updatedPost.Slug);
            Assert.AreEqual(origPost.Url, updatedPost.Url);
            Assert.AreNotEqual(originalTitle, updatedPost.Title);
        }

        [Test]
        public void UpdatePost_DoesNotChangeTitleOrURL_WhenSlugIsModified()
        {
            var originalSlug = origPost.Slug;

            origPost.Slug = "a-different-title";

            var updatedPost = auth.UpdatePost(origPost);

            Assert.AreEqual(origPost.Id, updatedPost.Id);
            Assert.AreEqual(origPost.Url, updatedPost.Url);
            Assert.AreEqual(origPost.Title, updatedPost.Title);
            Assert.AreNotEqual(originalSlug, updatedPost.Slug);
        }

        [Test]
        public void UpdatePost_UsesMobileDocAsSource_WhenNoHtmlProvided()
        {
            origPost.MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Random mobile doc content\"]]]]}";

            var updatedPost = auth.UpdatePost(origPost);

            Assert.AreEqual(origPost.Id, updatedPost.Id);
            Assert.IsNull(origPost.Html);
            Assert.IsNull(updatedPost.Html);
            Assert.IsNotNull(origPost.MobileDoc);
            Assert.IsNotNull(updatedPost.MobileDoc);
            Assert.That(updatedPost.MobileDoc.Contains("Random mobile doc content"));
        }

        [Test]
        public void UpdatePost_UsesHtmlAsSource_WhenHtmlProvided()
        {
            origPost.MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Random mobile doc content\"]]]]}";
            origPost.Html = "<p>I remember reading an article on dev.to last year</p>";

            var updatedPost = auth.UpdatePost(origPost);

            Assert.AreEqual(origPost.Id, updatedPost.Id);
            Assert.IsNull(updatedPost.Html);
            Assert.IsNotNull(origPost.MobileDoc);
            Assert.IsNotNull(updatedPost.MobileDoc);
            Assert.That(!updatedPost.MobileDoc.Contains("Random mobile doc content"));
            Assert.That(updatedPost.MobileDoc.Contains("I remember reading an article on dev.to last year"));
        }

        //[Test]
        //public void UpdatePost_IgnoresSendEmailWhenPublished_EvenWhenStatusChangesToScheduled()
        //{
        //    origPost.Status = "scheduled";
        //    origPost.PublishedAt = DateTime.UtcNow.AddYears(300);
        //    origPost.SendEmailWhenPublished = true;

        //    var post = auth.UpdatePost(origPost);

        //    Assert.AreEqual("scheduled", post.Status);
        //    Assert.IsFalse(post.SendEmailWhenPublished);
        //}

        [Test]
        public void UpdatePost_Succeeds_WhenPublishedAtIsChangedToBeEarlierThanOriginal()
        {
            Post origPost = null;

            var originalPublishedAt = DateTime.UtcNow.RemoveTicks();

            try
            {
                origPost = auth.CreatePost(
                    new Post
                    {
                        Title = "sample title",
                        PublishedAt = originalPublishedAt,
                    });

                origPost.PublishedAt = originalPublishedAt.AddYears(-1);

                var updatedPost = auth.UpdatePost(origPost);

                Assert.AreEqual(originalPublishedAt.AddYears(-1), updatedPost.PublishedAt);
            }
            finally
            {
                if (origPost != null)
                    auth.DeletePost(origPost.Id);
            }
        }
    }
}
