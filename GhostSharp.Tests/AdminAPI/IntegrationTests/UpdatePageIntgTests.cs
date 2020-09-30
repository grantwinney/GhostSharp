using GhostSharp.Entities;
using GhostSharp.Tests.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class UpdatePageIntgTests : TestBase
    {
        private GhostAdminAPI auth;
        private Post origPage = null;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);

            origPage = auth.CreatePage(
                new Post
                {
                    Title = "Sample page used for update page tests",
                    MetaTitle = "sample meta title",
                });
        }

        [TearDown]
        public void TearDown()
        {
            if (origPage != null)
                auth.DeletePage(origPage.Id);

            origPage = null;
        }

        [Test]
        public void UpdatePage_Succeeds_WhenAllFieldsChange()
        {
            Post origPage = null;

            var originalTitle = "Original Title";
            var originalSlug = "original-improbable-sluggy-slug-name-sluggish-1234235";
            var originalMobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Original Page Content\"]]]]}";
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
                origPage = auth.CreatePage(
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

                origPage.Title = "Updated Title";
                origPage.Slug = "updated-improbable-sluggy-slug-name-sluggish-1234235";
                origPage.MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Updated Page Content\"]]]]}";
                origPage.Html = "<h1>Updated</h1><p>Html</p>";
                origPage.FeatureImage = "updated_image.jpg";
                origPage.Featured = false;
                origPage.MetaTitle = "Updated Meta Title";
                origPage.MetaDescription = "Updated Meta Description";
                origPage.PublishedAt = originalPublishedAt.AddMinutes(20);
                origPage.CustomExcerpt = "Updated Excerpt";
                origPage.CodeInjectionHead = "Updated Header";
                origPage.CodeInjectionFoot = "Updated Footer";
                origPage.OgImage = "updated_og_image";
                origPage.OgTitle = "Updated OG Title";
                origPage.OgDescription = "Updated OG Description";
                origPage.TwitterImage = "updated_twitter_image";
                origPage.TwitterTitle = "Updated Twitter Title";
                origPage.TwitterDescription = "Updated Twitter Description";
                origPage.CustomTemplate = "Updated Custom Template";
                origPage.Excerpt = "Updated Excerpt";
                origPage.CanonicalUrl = "updated_canonical_url";
                origPage.Status = "scheduled";
                origPage.Visibility = "paid";
                origPage.EmailSubject = "updated email subject";

                var updatedPage = auth.UpdatePage(origPage);

                Assert.AreNotEqual(originalTitle, updatedPage.Title);
                Assert.AreNotEqual(originalSlug, updatedPage.Slug);
                Assert.AreNotEqual(originalMobileDoc, updatedPage.MobileDoc);
                Assert.AreNotEqual(originalHtml, updatedPage.Html);
                Assert.AreNotEqual(originalFeatureImage, updatedPage.FeatureImage);
                Assert.IsFalse(updatedPage.Featured);
                Assert.AreNotEqual(originalMetaTitle, updatedPage.MetaTitle);
                Assert.AreNotEqual(originalMetaDescription, updatedPage.MetaDescription);
                Assert.AreEqual(originalPublishedAt.AddMinutes(20).RemoveTicks(), updatedPage.PublishedAt);
                Assert.AreNotEqual(originalCustomExcerpt, updatedPage.CustomExcerpt);
                Assert.AreNotEqual(originalCodeInjectionHead, updatedPage.CodeInjectionHead);
                Assert.AreNotEqual(originalCodeInjectionFoot, updatedPage.CodeInjectionFoot);
                Assert.AreNotEqual(originalOgImage, updatedPage.OgImage);
                Assert.AreNotEqual(originalOgTitle, updatedPage.OgTitle);
                Assert.AreNotEqual(originalOgDescription, updatedPage.OgDescription);
                Assert.AreNotEqual(originalTwitterImage, updatedPage.TwitterImage);
                Assert.AreNotEqual(originalTwitterTitle, updatedPage.TwitterTitle);
                Assert.AreNotEqual(originalTwitterDescription, updatedPage.TwitterDescription);
                Assert.AreNotEqual(originalCustomTemplate, updatedPage.CustomTemplate);
                Assert.AreNotEqual(originalExcerpt, updatedPage.Excerpt);
                Assert.AreNotEqual(originalCanonicalUrl, updatedPage.CanonicalUrl);
                Assert.AreNotEqual(originalStatus, updatedPage.Status);
                Assert.AreNotEqual(originalVisibility, updatedPage.Visibility);
                Assert.AreNotEqual(originalEmailSubject, updatedPage.EmailSubject);
            }
            finally
            {
                if (origPage != null)
                    auth.DeletePage(origPage.Id);
            }
        }

        //[Test]
        //public void SendEmailWhenPublished_IsSentAsQueryParameter_ButStillDoesntSeemToWork()
        //{
        //    origPage.Status = "scheduled";
        //    origPage.PublishedAt = DateTime.UtcNow.AddYears(300);
        //    //origPage.SendEmailWhenPublished = true;

        //    var updatedPage = auth.UpdatePage(origPage);

        //    Assert.AreEqual(origPage.Id, updatedPage.Id);
        //    Assert.False(origPage.SendEmailWhenPublished);
        //    Assert.False(updatedPage.SendEmailWhenPublished);  // SHOULD BE TRUE
        //}

        //[Test]
        //public void UpdatePage_IgnoresSendEmailWhenPublished_EvenWhenStatusChangesToScheduled()
        //{
        //    origPage.Status = "scheduled";
        //    origPage.PublishedAt = DateTime.UtcNow.AddYears(300);
        //    origPage.SendEmailWhenPublished = true;

        //    var page = auth.UpdatePage(origPage);

        //    Assert.AreEqual("scheduled", page.Status);
        //    Assert.IsFalse(page.SendEmailWhenPublished);
        //}

        [Test]
        public void UpdatePage_DoesNotChangeSlugOrURL_WhenTitleIsModified()
        {
            var originalTitle = origPage.Title;

            origPage.Title = "a different title...";

            var updatedPage = auth.UpdatePage(origPage);

            Assert.AreEqual(origPage.Id, updatedPage.Id);
            Assert.AreEqual(origPage.Slug, updatedPage.Slug);
            Assert.AreEqual(origPage.Url, updatedPage.Url);
            Assert.AreNotEqual(originalTitle, updatedPage.Title);
        }

        [Test]
        public void UpdatePage_DoesNotChangeTitleOrURL_WhenSlugIsModified()
        {
            var originalSlug = origPage.Slug;

            origPage.Slug = "a-different-title";

            var updatedPage = auth.UpdatePage(origPage);

            Assert.AreEqual(origPage.Id, updatedPage.Id);
            Assert.AreEqual(origPage.Url, updatedPage.Url);
            Assert.AreEqual(origPage.Title, updatedPage.Title);
            Assert.AreNotEqual(originalSlug, updatedPage.Slug);
        }

        [Test]
        public void UpdatePage_UsesMobileDocAsSource_WhenNoHtmlProvided()
        {
            origPage.MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Random mobile doc content\"]]]]}";

            var updatedPage = auth.UpdatePage(origPage);

            Assert.AreEqual(origPage.Id, updatedPage.Id);
            Assert.IsNull(origPage.Html);
            Assert.IsNull(updatedPage.Html);
            Assert.IsNotNull(origPage.MobileDoc);
            Assert.IsNotNull(updatedPage.MobileDoc);
            Assert.That(updatedPage.MobileDoc.Contains("Random mobile doc content"));
        }

        [Test]
        public void UpdatePage_UsesHtmlAsSource_WhenHtmlProvided()
        {
            origPage.MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Random mobile doc content\"]]]]}";
            origPage.Html = "<p>I remember reading an article on dev.to last year</p>";

            var updatedPage = auth.UpdatePage(origPage);

            Assert.AreEqual(origPage.Id, updatedPage.Id);
            Assert.IsNull(updatedPage.Html);
            Assert.IsNotNull(origPage.MobileDoc);
            Assert.IsNotNull(updatedPage.MobileDoc);
            Assert.That(!updatedPage.MobileDoc.Contains("Random mobile doc content"));
            Assert.That(updatedPage.MobileDoc.Contains("I remember reading an article on dev.to last year"));
        }

        [Test]
        public void UpdatePage_Succeeds_WhenPublishedAtIsChangedToBeEarlierThanOriginal()
        {
            Post origPage = null;

            var originalPublishedAt = DateTime.UtcNow.RemoveTicks();

            try
            {
                origPage = auth.CreatePage(
                    new Post
                    {
                        Title = "sample title",
                        PublishedAt = originalPublishedAt,
                    });

                origPage.PublishedAt = originalPublishedAt.AddYears(-1);

                var updatedPage = auth.UpdatePage(origPage);

                Assert.AreEqual(originalPublishedAt.AddYears(-1), updatedPage.PublishedAt);
            }
            finally
            {
                if (origPage != null)
                    auth.DeletePage(origPage.Id);
            }
        }
    }
}
