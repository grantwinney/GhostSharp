using GhostSharp.Entities;
using GhostSharp.Tests.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class CreatePageIntgTests : TestBase
    {
        private GhostAdminAPI auth;
        private string newPageId = null;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [TearDown]
        public void TearDown()
        {
            if (newPageId != null)
                auth.DeletePage(newPageId);

            newPageId = null;
        }

        [Test]
        public void CreatePage_CreatesBasicPage()
        {
            var expectedPage = new Post
            {
                Title = "This is a test page",
                MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"My page content. Work in progress...\"]]]]}",
                Status = "draft",
            };

            var actualPage = auth.CreatePage(expectedPage);

            newPageId = actualPage.Id;

            Assert.AreEqual(expectedPage.Title, actualPage.Title);
            Assert.AreEqual(expectedPage.MobileDoc, actualPage.MobileDoc);
            Assert.AreEqual(expectedPage.Status, actualPage.Status);
            Assert.AreEqual(actualPage.Title.Replace(' ', '-').ToLower(), actualPage.Slug);

            Assert.IsNotNull(actualPage.Uuid);
            Assert.IsTrue(Guid.TryParse(actualPage.Uuid, out _));
            Assert.IsNotNull(actualPage.CommentId);
            Assert.IsNotNull(actualPage.CreatedAt);
            Assert.IsNotNull(actualPage.UpdatedAt);
            Assert.IsNotNull(actualPage.Url);
            Assert.IsTrue(actualPage.Url.StartsWith($"{Host}p/"));
            Assert.IsNotNull(actualPage.Excerpt);
            Assert.AreEqual("My page content. Work in progress...", actualPage.Excerpt);
            Assert.IsNotNull(actualPage.PrimaryAuthor);
            Assert.IsNotNull(actualPage.Authors);
            Assert.AreEqual(1, actualPage.Authors.Count);

            Assert.IsNull(actualPage.MetaTitle);
            Assert.IsNull(actualPage.MetaDescription);
            Assert.IsNull(actualPage.CodeInjectionHead);
            Assert.IsNull(actualPage.CodeInjectionFoot);
            Assert.IsNull(actualPage.OgImage);
            Assert.IsNull(actualPage.OgTitle);
            Assert.IsNull(actualPage.TwitterImage);
            Assert.IsNull(actualPage.TwitterTitle);
            Assert.IsNull(actualPage.CustomTemplate);
            Assert.IsNull(actualPage.Html);
            Assert.IsNull(actualPage.PublishedAt);
            Assert.IsNull(actualPage.PlainText);
            Assert.IsNull(actualPage.FeatureImage);
            Assert.IsNull(actualPage.CustomExcerpt);
            Assert.IsNull(actualPage.OgDescription);
            Assert.IsNull(actualPage.TwitterDescription);
            Assert.IsNull(actualPage.CanonicalUrl);
            Assert.IsNull(actualPage.PrimaryTag);

            Assert.IsEmpty(actualPage.Tags);
            Assert.IsFalse(actualPage.Featured);
        }

        [Test]
        public void CreatePage_CreatesFullPage()
        {
            var title = "Original Title";
            var slug = "original-improbable-sluggy-slug-name-sluggish-1234235";
            var mobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Original Page Content\"]]]]}";
            var html = "<h1>Original</h1><p>Html</p>";
            var featureImage = "original_image.jpg";
            var metaTitle = "Original Meta Title";
            var metaDescription = "Original Meta Description";
            var publishedAt = DateTime.UtcNow.AddMinutes(10);
            var customExcerpt = "Original Excerpt";
            var codeInjectionHead = "Original Header";
            var codeInjectionFoot = "Original Footer";
            var ogImage = "original_og_image";
            var ogTitle = "Original OG Title";
            var ogDescription = "Original OG Description";
            var twitterImage = "original_twitter_image";
            var twitterTitle = "Original Twitter Title";
            var twitterDescription = "Original Twitter Description";
            var customTemplate = "Original Custom Template";
            var canonicalUrl = "original_canonical_url";
            var excerpt = "Original Excerpt";
            var status = "draft";
            var visibility = "public";

            var page = auth.CreatePage(
                new Post
                {
                    Title = title,
                    Slug = slug,
                    MobileDoc = mobileDoc,
                    Html = html,
                    FeatureImage = featureImage,
                    Featured = true,
                    MetaTitle = metaTitle,
                    MetaDescription = metaDescription,
                    PublishedAt = publishedAt,
                    CustomExcerpt = customExcerpt,
                    CodeInjectionHead = codeInjectionHead,
                    CodeInjectionFoot = codeInjectionFoot,
                    OgImage = ogImage,
                    OgTitle = ogTitle,
                    OgDescription = ogDescription,
                    TwitterImage = twitterImage,
                    TwitterTitle = twitterTitle,
                    TwitterDescription = twitterDescription,
                    CustomTemplate = customTemplate,
                    Excerpt = excerpt,
                    CanonicalUrl = canonicalUrl,
                    Status = status,
                    Visibility = visibility,
                });

            newPageId = page.Id;

            Assert.AreEqual(title, page.Title);
            Assert.AreEqual(slug, page.Slug);
            Assert.AreNotEqual(mobileDoc, page.MobileDoc);                                        // Html overrides MobileDoc
            Assert.That(page.MobileDoc.Contains("Original") && page.MobileDoc.Contains("Html"));  // Html overrides MobileDoc
            Assert.IsNull(page.Html);                                                             // Html isn't returned unless you specify
            Assert.AreEqual(featureImage, page.FeatureImage);
            Assert.IsTrue(page.Featured);
            Assert.AreEqual(metaTitle, page.MetaTitle);
            Assert.AreEqual(metaDescription, page.MetaDescription);
            Assert.AreEqual(publishedAt.RemoveTicks(), page.PublishedAt.Value);
            Assert.AreEqual(customExcerpt, page.CustomExcerpt);
            Assert.IsNull(page.CodeInjectionHead);  // Works, but value doesn't get returned
            Assert.IsNull(page.CodeInjectionFoot);  // Works, but value doesn't get returned
            Assert.AreEqual(ogImage, page.OgImage);
            Assert.AreEqual(ogTitle, page.OgTitle);
            Assert.AreEqual(ogDescription, page.OgDescription);
            Assert.AreEqual(twitterImage, page.TwitterImage);
            Assert.AreEqual(twitterTitle, page.TwitterTitle);
            Assert.AreEqual(twitterDescription, page.TwitterDescription);
            Assert.AreEqual(customTemplate, page.CustomTemplate);
            Assert.AreEqual(excerpt, page.Excerpt);
            Assert.AreEqual(canonicalUrl, page.CanonicalUrl);
            Assert.AreEqual(status, page.Status);
            Assert.AreEqual(visibility, page.Visibility);
        }

        [Test]
        public void CreatePage_SetsMissingStatusToDraft()
        {
            var expectedPage = new Post
            {
                Title = "This is a test page with missing status"
            };

            var actualPage = auth.CreatePage(expectedPage);

            newPageId = actualPage.Id;

            Assert.AreEqual(expectedPage.Title, actualPage.Title);
            Assert.AreEqual("draft", actualPage.Status);
        }

        [Test]
        public void CreatePage_SetsMissingAuthorToOwner()
        {
            var expectedPage = new Post
            {
                Title = "This is a test page with missing author",
                Status = "draft"
            };

            var actualPage = auth.CreatePage(expectedPage);

            newPageId = actualPage.Id;

            Assert.AreEqual(expectedPage.Title, actualPage.Title);

            Assert.IsNotNull(actualPage.PrimaryAuthor);
            Assert.AreEqual(ValidAuthor1Id, actualPage.PrimaryAuthor.Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPage.PrimaryAuthor.Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPage.PrimaryAuthor.Name);
            Assert.AreEqual(ValidAuthor1Url, actualPage.PrimaryAuthor.Url);

            Assert.IsNotNull(actualPage.Authors);
            Assert.AreEqual(1, actualPage.Authors.Count);
            Assert.AreEqual(ValidAuthor1Id, actualPage.Authors[0].Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPage.Authors[0].Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPage.Authors[0].Name);
            Assert.AreEqual(ValidAuthor1Url, actualPage.Authors[0].Url);
        }

        [Test]
        public void CreatePage_CanUseHtmlInsteadOfMobileDoc()
        {
            var expectedPage = new Post
            {
                Title = "This is a test page using html instead of mobiledoc",
                Html = "<h1>Test Header</h1><b>Test Body</b>",
                Status = "draft"
            };

            var actualPage = auth.CreatePage(expectedPage);

            newPageId = actualPage.Id;

            Assert.AreEqual(expectedPage.Title, actualPage.Title);
            Assert.AreEqual(expectedPage.Status, actualPage.Status);
            Assert.AreEqual(actualPage.Title.Replace(' ', '-').ToLower(), actualPage.Slug);
            Assert.AreEqual("Test Header\nTest Body", actualPage.Excerpt);

            Assert.AreEqual(
                "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[[\"b\"]],\"sections\":[[1,\"h1\",[[0,[],0,\"Test Header\"]]],[1,\"p\",[[0,[0],1,\"Test Body\"]]]]}",
                actualPage.MobileDoc);

            Assert.IsNull(actualPage.Html);
            Assert.IsNull(actualPage.PlainText);
        }

        [Test]
        public void CreatePage_RequiresOnlyATitle()
        {
            var expectedPage = new Post
            {
                Title = "This is a test page with nothing but title"
            };

            var actualPage = auth.CreatePage(expectedPage);

            newPageId = actualPage.Id;

            Assert.AreEqual(expectedPage.Title, actualPage.Title);
            Assert.AreEqual("draft", actualPage.Status);
            Assert.AreEqual(actualPage.Title.Replace(' ', '-').ToLower(), actualPage.Slug);

            Assert.IsNull(actualPage.Html);
            Assert.IsNotNull(actualPage.MobileDoc);
            Assert.IsNull(actualPage.PlainText);
        }

        [Test]
        public void CreatePage_RequiresATitle()
        {
            var exception = Assert.Throws<GhostSharpException>(() => auth.CreatePage(new Post { Title = null, Status = "draft" }));

            Assert.That(exception.Message.Contains("Validation error, cannot save page."));
            Assert.That(exception.Message.Contains("missingProperty:title"));
        }

        [Test]
        public void CreatePage_CannotProcessAnEmptyPayload()
        {
            var exception = Assert.Throws<GhostSharpException>(() => auth.CreatePage(new Post()));

            Assert.That(exception.Message.StartsWith("Request not understood error, cannot save page."));
        }

        [Test]
        public void CreatePage_CanCreatePage_WithHtmlBody()
        {
            var expectedPage = new Post
            {
                Title = "This is a test page with an html body",
                Html = "<b>This is an html body!!</b>",
                Status = "draft"
            };

            var actualPage = auth.CreatePage(expectedPage);

            newPageId = actualPage.Id;

            Assert.AreEqual(expectedPage.Title, actualPage.Title);
            Assert.AreEqual(
                "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[[\"b\"]],\"sections\":[[1,\"p\",[[0,[0],1,\"This is an html body!!\"]]]]}",
                actualPage.MobileDoc);
            Assert.IsNull(actualPage.Html);
        }

        [Test]
        public void CreatePage_CanAttachTagsUsingLongForm_WhenUsingTagsField()
        {
            var expectedPage = new Post
            {
                Title = "This is a test page with long form tags",
                Status = "draft",
                Tags = new List<Tag>
                {
                    new Tag { Name = ValidTag1Name },
                    new Tag { Name = ValidTag2Name }
                }
            };

            var actualPage = auth.CreatePage(expectedPage);

            newPageId = actualPage.Id;

            Assert.AreEqual(expectedPage.Title, actualPage.Title);

            Assert.AreEqual(ValidTag1Id, actualPage.PrimaryTag.Id);
            Assert.AreEqual(ValidTag1Slug, actualPage.PrimaryTag.Slug);
            Assert.AreEqual(ValidTag1Name, actualPage.PrimaryTag.Name);

            Assert.AreEqual(ValidTag1Id, actualPage.Tags[0].Id);
            Assert.AreEqual(ValidTag1Slug, actualPage.Tags[0].Slug);
            Assert.AreEqual(ValidTag1Name, actualPage.Tags[0].Name);

            Assert.AreEqual(ValidTag2Id, actualPage.Tags[1].Id);
            Assert.AreEqual(ValidTag2Slug, actualPage.Tags[1].Slug);
            Assert.AreEqual(ValidTag2Name, actualPage.Tags[1].Name);
        }

        [Test]
        public void CreatePage_CanAttachAuthorsUsingLongForm_WhenUsingAuthorsField()
        {
            var expectedPage = new Post
            {
                Title = "This is a test page with long form authors",
                Status = "draft",
                Authors = new List<Author>
                {
                    new Author { Id = ValidAuthor1Id },
                    new Author { Slug = ValidAuthor2Slug }
                }
            };

            var actualPage = auth.CreatePage(expectedPage);

            newPageId = actualPage.Id;

            Assert.AreEqual(expectedPage.Title, actualPage.Title);

            Assert.AreEqual(ValidAuthor1Id, actualPage.PrimaryAuthor.Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPage.PrimaryAuthor.Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPage.PrimaryAuthor.Name);

            Assert.AreEqual(ValidAuthor1Id, actualPage.Authors[0].Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPage.Authors[0].Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPage.Authors[0].Name);

            Assert.AreEqual(ValidAuthor2Id, actualPage.Authors[1].Id);
            Assert.AreEqual(ValidAuthor2Slug, actualPage.Authors[1].Slug);
            Assert.AreEqual(ValidAuthor2Name, actualPage.Authors[1].Name);
        }

        [Test]
        public void CreatePage_CanNotAttachTagUsingLongForm_WhenUsingPrimaryTag()
        {
            var actualPage = auth.CreatePage(
                new Post
                {
                    Title = "This is a test page with long form tag by id",
                    PrimaryTag = new Tag { Id = ValidTag1Id },
                    Status = "draft"
                });

            newPageId = actualPage.Id;

            Assert.IsEmpty(actualPage.Tags);
            Assert.IsNull(actualPage.PrimaryTag);
        }

        [Test]
        public void CreatePage_CanNotAttachAuthorUsingLongForm_WhenUsingPrimaryAuthor()
        {
            var actualPage = auth.CreatePage(
                new Post
                {
                    Title = "This is a test page with long form author using id",
                    PrimaryAuthor = new Author { Id = ValidAuthor2Id },
                    Status = "draft"
                });

            newPageId = actualPage.Id;

            // It attaches the default owner, not the alternate author I specified above
            Assert.AreEqual(ValidAuthor1Id, actualPage.PrimaryAuthor.Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPage.PrimaryAuthor.Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPage.PrimaryAuthor.Name);

            Assert.AreEqual(ValidAuthor1Id, actualPage.Authors[0].Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPage.Authors[0].Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPage.Authors[0].Name);
        }

        [Test]
        public void CreatePage_AttachesOwnerRoleAsAuthor_WhenSpecifiedAuthorNotFound()
        {
            var expectedPage = new Post
            {
                Title = "This is a test page with invalid author",
                PrimaryAuthor = new Author { Id = InvalidAuthorId }
            };

            var actualPage = auth.CreatePage(expectedPage);

            newPageId = actualPage.Id;

            Assert.AreEqual(expectedPage.Title, actualPage.Title);
            Assert.AreEqual("draft", actualPage.Status);

            Assert.AreEqual(ValidAuthor1Id, actualPage.PrimaryAuthor.Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPage.PrimaryAuthor.Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPage.PrimaryAuthor.Name);

            Assert.AreEqual(ValidAuthor1Id, actualPage.Authors[0].Id);
            Assert.AreEqual(ValidAuthor1Slug, actualPage.Authors[0].Slug);
            Assert.AreEqual(ValidAuthor1Name, actualPage.Authors[0].Name);

            Assert.AreEqual(1, actualPage.Authors.Count);
        }

        [Test]
        public void CreatePage_UsesMobileDocAsSource_WhenNoHtmlProvided()
        {
            var page = auth.CreatePage(
                new Post
                {
                    Title = "This is a test page",
                    MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Random mobile doc content\"]]]]}",
                    Status = "draft"
                });

            newPageId = page.Id;

            Assert.IsNull(page.Html);
            Assert.IsNotNull(page.MobileDoc);
            Assert.That(page.MobileDoc.Contains("Random mobile doc content"));
        }

        [Test]
        public void CreatePage_UsesHtmlAsSource_WhenHtmlProvided()
        {
            var page = auth.CreatePage(
                new Post
                {
                    Title = "This is a test page",
                    MobileDoc = "{\"version\":\"0.3.1\",\"atoms\":[],\"cards\":[],\"markups\":[],\"sections\":[[1,\"p\",[[0,[],0,\"Random mobile doc content\"]]]]}",
                    Html = "<p>I remember reading an article on dev.to last year</p>",
                    Status = "draft"
                });

            newPageId = page.Id;

            Assert.IsNull(page.Html);
            Assert.IsNotNull(page.MobileDoc);
            Assert.That(!page.MobileDoc.Contains("Random mobile doc content"));
            Assert.That(page.MobileDoc.Contains("I remember reading an article on dev.to last year"));
        }

        [Test]
        public void CreatePage_IgnoresEmailSubject_WhenOneIsProvided()
        {
            // This seems to work with Posts just fine, but Pages are treated differently in the system, as generally static entities
            // that are excluded from all feeds. Ref: https://ghost.org/docs/concepts/pages/

            var title = "Original Title";

            var page = auth.CreatePage(
                new Post
                {
                    Title = title,
                    EmailSubject = "original email subject",
                });

            newPageId = page.Id;

            Assert.AreEqual(title, page.Title);
            Assert.IsNull(page.EmailSubject);
        }

        //[Test]
        //public void CreatePage_IgnoresSendEmailWhenPublished_EvenWhenStatusIsScheduled()
        //{
        //    var page = auth.CreatePage(
        //        new Post
        //        {
        //            Title = "This is a test page",
        //            Status = "scheduled",
        //            PublishedAt = DateTime.UtcNow.AddYears(300),
        //            SendEmailWhenPublished = true,
        //        });

        //    newPageId = page.Id;

        //    Assert.AreEqual("scheduled", page.Status);
        //    Assert.IsFalse(page.SendEmailWhenPublished);
        //}
    }
}
