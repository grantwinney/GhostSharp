using GhostSharp.Entities;
using GhostSharp.Enums;
using GhostSharp.QueryParams;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GhostSharp.Tests.ContentAPI.IntegrationTests
{
    [TestFixture]
    public class GetPageIntgTests : TestBase
    {
        private const int MINIMUM_PAGE_COUNT_THRESHHOLD = 4;

        private GhostAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostContentAPI(Host, ValidContentApiKey);
        }

        [Test]
        public void GetPageById_ReturnsMatchingPage()
        {
            var page = auth.GetPageById(ValidPage1Id);

            Assert.AreEqual(ValidPage1Id, page.Id);
            Assert.AreEqual(ValidPage1Slug, page.Slug);
            Assert.AreEqual(ValidPage1Url, page.Url);

            Assert.IsNotNull(page.Uuid);
            Assert.IsNotNull(page.Html);
            Assert.IsNotNull(page.CommentId);
            Assert.IsNotNull(page.CreatedAt);
            Assert.IsNotNull(page.UpdatedAt);
            Assert.IsNotNull(page.PublishedAt);
            Assert.IsNotNull(page.Url);
            Assert.IsNotNull(page.Excerpt);

            Assert.IsNull(page.MetaTitle);
            Assert.IsNull(page.CodeInjectionHead);
            Assert.IsNull(page.CodeInjectionFoot);
            Assert.IsNull(page.OgImage);
            Assert.IsNull(page.OgTitle);
            Assert.IsNull(page.TwitterImage);
            Assert.IsNull(page.TwitterTitle);
            Assert.IsNull(page.CustomTemplate);
            Assert.IsNull(page.PrimaryAuthor);
            Assert.IsNull(page.PrimaryTag);
            Assert.IsNull(page.Authors);
            Assert.IsNull(page.Tags);
        }

        [Test]
        public void GetPageById_ReturnsAuthors_WhenIncludingAuthors()
        {
            var page = auth.GetPageById(ValidPage1Id, new PostQueryParams { IncludeAuthors = true });

            Assert.AreEqual(ValidPage1Id, page.Id);
            Assert.AreEqual(ValidPage1Slug, page.Slug);
            Assert.AreEqual(ValidPage1Url, page.Url);

            Assert.IsNotNull(page.Authors);
            Assert.IsNotNull(page.PrimaryAuthor);
            Assert.AreEqual(ValidPage1Author, page.Authors[0].Id);
            Assert.AreEqual(ValidPage1Author, page.PrimaryAuthor.Id);

            Assert.IsNull(page.Tags);
            Assert.IsNull(page.PrimaryTag);
        }

        [Test]
        public void GetPageById_ReturnsTags_WhenIncludingTags()
        {
            var page = auth.GetPageById(ValidPage1Id, new PostQueryParams { IncludeTags = true });

            Assert.AreEqual(ValidPage1Id, page.Id);
            Assert.AreEqual(ValidPage1Slug, page.Slug);
            Assert.AreEqual(ValidPage1Url, page.Url);

            Assert.IsNotNull(page.Tags);
        }

        [Test]
        public void GetPageById_ReturnsAuthorsAndTags_WhenIncludingAuthorsAndTags()
        {
            var page = auth.GetPageById(ValidPage1Id, new PostQueryParams { IncludeAuthors = true, IncludeTags = true });

            Assert.AreEqual(ValidPage1Id, page.Id);
            Assert.AreEqual(ValidPage1Slug, page.Slug);
            Assert.AreEqual(ValidPage1Url, page.Url);

            Assert.IsNotNull(page.Tags);

            Assert.IsNotNull(page.Authors);
            Assert.IsNotNull(page.PrimaryAuthor);
            Assert.AreEqual(ValidPage1Author, page.Authors[0].Id);
            Assert.AreEqual(ValidPage1Author, page.PrimaryAuthor.Id);
        }

        [Test]
        public void GetPageById_ReturnsLimitedFields_WhenFieldsSpecified()
        {
            var page = auth.GetPageById(ValidPage1Id, new PostQueryParams { Fields = PostFields.Id | PostFields.Slug });

            Assert.AreEqual(ValidPage1Id, page.Id);
            Assert.AreEqual(ValidPage1Slug, page.Slug);
            Assert.IsNull(page.Title);
            Assert.IsNull(page.Url);
        }

        [Test]
        public void GetPageBySlug_ReturnsMatchingPage()
        {
            var page = auth.GetPageBySlug(ValidPage1Slug);

            Assert.AreEqual(ValidPage1Id, page.Id);
            Assert.AreEqual(ValidPage1Slug, page.Slug);
            Assert.AreEqual(ValidPage1Url, page.Url);

            Assert.IsNotNull(page.Uuid);
            Assert.IsNotNull(page.Html);
            Assert.IsNotNull(page.CommentId);
            Assert.IsNotNull(page.CreatedAt);
            Assert.IsNotNull(page.UpdatedAt);
            Assert.IsNotNull(page.PublishedAt);
            Assert.IsNotNull(page.Url);
            Assert.IsNotNull(page.Excerpt);
            Assert.IsNotNull(page.Title);

            Assert.IsNull(page.MetaTitle);
            Assert.IsNull(page.CodeInjectionHead);
            Assert.IsNull(page.CodeInjectionFoot);
            Assert.IsNull(page.OgImage);
            Assert.IsNull(page.OgTitle);
            Assert.IsNull(page.TwitterImage);
            Assert.IsNull(page.TwitterTitle);
            Assert.IsNull(page.CustomTemplate);
            Assert.IsNull(page.PrimaryAuthor);
            Assert.IsNull(page.PrimaryTag);
            Assert.IsNull(page.Authors);
            Assert.IsNull(page.Tags);
        }

        [Test]
        public void GetPageBySlug_ReturnsAuthors_WhenIncludingAuthors()
        {
            var page = auth.GetPageBySlug(ValidPage1Slug, new PostQueryParams { IncludeAuthors = true });

            Assert.AreEqual(ValidPage1Id, page.Id);
            Assert.AreEqual(ValidPage1Slug, page.Slug);
            Assert.AreEqual(ValidPage1Url, page.Url);

            Assert.IsNotNull(page.Authors);
            Assert.IsNotNull(page.PrimaryAuthor);
            Assert.AreEqual(ValidPage1Author, page.Authors[0].Id);
            Assert.AreEqual(ValidPage1Author, page.PrimaryAuthor.Id);

            Assert.IsNull(page.Tags);
            Assert.IsNull(page.PrimaryTag);
        }

        [Test]
        public void GetPageBySlug_ReturnsTags_WhenIncludingTags()
        {
            var page = auth.GetPageBySlug(ValidPage1Slug, new PostQueryParams { IncludeTags = true });

            Assert.AreEqual(ValidPage1Id, page.Id);
            Assert.AreEqual(ValidPage1Slug, page.Slug);
            Assert.AreEqual(ValidPage1Url, page.Url);

            Assert.IsNotNull(page.Tags);

            Assert.IsNull(page.Authors);
            Assert.IsNull(page.PrimaryAuthor);
        }

        [Test]
        public void GetPageBySlug_ReturnsAuthorsAndTags_WhenIncludingAuthorsAndTags()
        {
            var page = auth.GetPageBySlug(ValidPage1Slug, new PostQueryParams { IncludeAuthors = true, IncludeTags = true });

            Assert.AreEqual(ValidPage1Id, page.Id);
            Assert.AreEqual(ValidPage1Slug, page.Slug);
            Assert.AreEqual(ValidPage1Url, page.Url);

            Assert.IsNotNull(page.Tags);

            Assert.IsNotNull(page.Authors);
            Assert.IsNotNull(page.PrimaryAuthor);
            Assert.AreEqual(ValidPage1Author, page.Authors[0].Id);
            Assert.AreEqual(ValidPage1Author, page.PrimaryAuthor.Id);
        }

        [Test]
        public void GetPageBySlug_ReturnsLimitedFields_WhenFieldsSpecified()
        {
            var page = auth.GetPageBySlug(ValidPage1Slug, new PostQueryParams { Fields = PostFields.Id | PostFields.Slug });

            Assert.AreEqual(ValidPage1Id, page.Id);
            Assert.AreEqual(ValidPage1Slug, page.Slug);
            Assert.IsNull(page.Title);
            Assert.IsNull(page.Url);
        }

        [Test]
        public void GetPages_ReturnsLimitedPages_WhenLimitSpecified()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var pageResponse = auth.GetPages(new PostQueryParams { Limit = 1, Fields = PostFields.Id });

            Assert.AreEqual(1, pageResponse.Pages.Count);
        }

        [Test]
        public void GetPages_ReturnsLimitedFields_WhenFieldsSpecified()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var page = auth.GetPages(new PostQueryParams { Limit = 1, Fields = PostFields.Id }).Pages[0];

            Assert.IsNotNull(page.Id);

            Assert.IsNull(page.Authors);
            Assert.IsNull(page.CodeInjectionFoot);
            Assert.IsNull(page.CodeInjectionHead);
            Assert.IsNull(page.CommentId);
            Assert.IsNull(page.CreatedAt);
            Assert.IsNull(page.CustomExcerpt);
            Assert.IsNull(page.CustomTemplate);
            Assert.IsNull(page.Excerpt);
            Assert.IsNull(page.Featured);
            Assert.IsNull(page.FeatureImage);
            Assert.IsNull(page.Html);
            Assert.IsNull(page.MetaDescription);
            Assert.IsNull(page.MetaTitle);
            Assert.IsNull(page.MobileDoc);
            Assert.IsNull(page.OgDescription);
            Assert.IsNull(page.OgImage);
            Assert.IsNull(page.OgTitle);
            Assert.IsNull(page.PlainText);
            Assert.IsNull(page.PrimaryAuthor);
            Assert.IsNull(page.PrimaryTag);
            Assert.IsNull(page.PublishedAt);
            Assert.IsNull(page.Slug);
            Assert.IsNull(page.Tags);
            Assert.IsNull(page.Title);
            Assert.IsNull(page.TwitterDescription);
            Assert.IsNull(page.TwitterImage);
            Assert.IsNull(page.TwitterTitle);
            Assert.IsNull(page.UpdatedAt);
            Assert.IsNull(page.Url);
            Assert.IsNull(page.Uuid);
        }

        [Test]
        public void GetPages_ReturnsAuthors_WhenIncludingAuthors()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var page = auth.GetPages(new PostQueryParams { Limit = 1, IncludeAuthors = true }).Pages[0];

            Assert.IsNotNull(page.Authors);
            Assert.IsNotNull(page.PrimaryAuthor);

            Assert.IsNull(page.Tags);
            Assert.IsNull(page.PrimaryTag);
        }

        [Test]
        public void GetPages_ReturnsTags_WhenIncludingTags()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var page = auth.GetPages(new PostQueryParams { Limit = 1, IncludeTags = true }).Pages[0];

            Assert.IsNotNull(page.Tags);

            Assert.IsNull(page.Authors);
            Assert.IsNull(page.PrimaryAuthor);
        }

        [Test]
        public void GetPages_ReturnsAuthorsAndTags_WhenIncludingAuthorsAndTags()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var page = auth.GetPages(new PostQueryParams { Limit = 1, IncludeTags = true, IncludeAuthors = true }).Pages[0];

            Assert.IsNotNull(page.Tags);

            Assert.IsNotNull(page.Authors);
            Assert.IsNotNull(page.PrimaryAuthor);
        }

        [Test]
        public void GetPages_ReturnsAllPages_WhenNoLimitIsTrue()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var pageResponse = auth.GetPages(new PostQueryParams { Limit = 1, NoLimit = true, Fields = PostFields.Id });

            Assert.GreaterOrEqual(pageResponse.Pages.Count, MINIMUM_PAGE_COUNT_THRESHHOLD);
        }

        [Test]
        public void GetPages_ReturnsExpectedPage_WhenOrderingByField()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var page = auth.GetPages(new PostQueryParams { Limit = 1, Order = new List<Tuple<PostFields, OrderDirection>> { Tuple.Create(PostFields.CreatedAt, OrderDirection.asc) } }).Pages[0];

            // potentially fragile
            Assert.AreEqual("5e90d3eb1318020e53971b5b", page.Id);
        }

        [Test]
        public void GetPages_ReturnsExpectedPage_WhenGettingSecondPage()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var page = auth.GetPages(new PostQueryParams { Page = 2, Limit = 2, Order = new List<Tuple<PostFields, OrderDirection>> { Tuple.Create(PostFields.CreatedAt, OrderDirection.asc) }, Fields = PostFields.Id }).Pages[0];

            // potentially fragile
            Assert.AreEqual("5e90d3eb1318020e53971bfd", page.Id);
        }

        [Test]
        public void GetPages_ReturnsExpectedPages_WhenApplyingFilter()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            var pageResponse = auth.GetPages(new PostQueryParams { Filter = $"slug:[{ValidPage1Slug}]" });
            Assert.AreEqual(1, pageResponse.Pages.Count);

            var page = pageResponse.Pages[0];

            Assert.AreEqual(ValidPage1Id, page.Id);
            Assert.AreEqual(ValidPage1Slug, page.Slug);
            Assert.AreEqual(ValidPage1Url, page.Url);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPages_ThrowsException_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostContentAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPages());
            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Unknown Content API Key", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetPages_ReturnsNull_WhenKeyIsInvalid_AndGhostExceptionsSuppressed(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostContentAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            Assert.IsNull(auth.GetPages());
            Assert.IsNotNull(auth.LastException);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPageById_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPageById(InvalidPageId));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Resource not found error, cannot read page.", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetPageById_ReturnsNull_WhenKeyIsInvalid_AndGhostExceptionsSuppressed(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetPageById(InvalidPageId));
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetPageBySlug_ThrowsGhostSharpException_WhenSlugIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetPageBySlug(InvalidPageSlug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Resource not found error, cannot read page.", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetPageBySlug_ReturnsNull_WhenKeyIsInvalid_AndGhostExceptionsSuppressed(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetPageBySlug(InvalidPageSlug));
        }
    }
}
