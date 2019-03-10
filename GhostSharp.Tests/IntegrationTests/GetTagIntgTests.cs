using GhostSharp.Entities;
using GhostSharp.Enums;
using GhostSharp.QueryParams;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GhostSharp.Tests.IntegrationTests
{
    [TestFixture]
    public class GetTagIntgTests : TestBase
    {
        private const int MINIMUM_TAG_AMOUNT_THRESHHOLD = 200;

        private GhostAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAPI(Host, ValidApiKey);
        }

        [Test]
        public void GetTagById_ReturnsMatchingTag()
        {
            var tag = auth.GetTagById(ValidTag1Id);

            Assert.AreEqual(ValidTag1Id, tag.Id);
            Assert.AreEqual(ValidTag1Slug, tag.Slug);
            Assert.AreEqual(ValidTag1Name, tag.Name);
            Assert.AreEqual(ValidTag1Description, tag.Description);
            Assert.AreEqual(ValidTag1FeatureImage, tag.FeatureImage);
            Assert.AreEqual(ValidTag1Visibility, tag.Visibility);
            Assert.AreEqual(ValidTag1MetaTitle, tag.MetaTitle);
            Assert.AreEqual(ValidTag1MetaDescription, tag.MetaDescription);
            Assert.AreEqual(null, tag.Count);
            Assert.AreEqual(ValidTag1Url, tag.Url);
        }

        [Test]
        public void GetTagById_ReturnsPostCount_WhenIncludingCountPosts()
        {
            var tag = auth.GetTagById(ValidTag1Id, new TagQueryParams { IncludePostCount = true });

            Assert.AreEqual(ValidTag1Id, tag.Id);
            Assert.AreEqual(ValidTag1Slug, tag.Slug);
            Assert.AreEqual(ValidTag1Name, tag.Name);
            Assert.AreEqual(ValidTag1Description, tag.Description);
            Assert.AreEqual(ValidTag1FeatureImage, tag.FeatureImage);
            Assert.AreEqual(ValidTag1Visibility, tag.Visibility);
            Assert.AreEqual(ValidTag1MetaTitle, tag.MetaTitle);
            Assert.AreEqual(ValidTag1MetaDescription, tag.MetaDescription);
            Assert.AreEqual(ValidTag1PostCount, tag.Count.Posts);
            Assert.AreEqual(ValidTag1Url, tag.Url);
        }

        [Test]
        public void GetTagById_ReturnsLimitedFields_WhenFieldsSpecified_ForIndividualRequest()
        {
            var tag = auth.GetTagById(ValidTag1Id, new TagQueryParams { Fields = TagFields.Id | TagFields.Slug | TagFields.Url });

            Assert.AreEqual(ValidTag1Id, tag.Id);
            Assert.AreEqual(ValidTag1Slug, tag.Slug);
            Assert.AreEqual(ValidTag1Url, tag.Url);

            Assert.IsNull(tag.Name);
            Assert.IsNull(tag.Description);
            Assert.IsNull(tag.FeatureImage);
            Assert.IsNull(tag.Visibility);
            Assert.IsNull(tag.MetaTitle);
            Assert.IsNull(tag.MetaDescription);
            Assert.IsNull(tag.Count);
        }

        [Test]
        public void GetTagBySlug_ReturnsMatchingTag()
        {
            var tag = auth.GetTagBySlug(ValidTag1Slug);

            Assert.AreEqual(ValidTag1Id, tag.Id);
            Assert.AreEqual(ValidTag1Slug, tag.Slug);
            Assert.AreEqual(ValidTag1Name, tag.Name);
            Assert.AreEqual(ValidTag1Description, tag.Description);
            Assert.AreEqual(ValidTag1FeatureImage, tag.FeatureImage);
            Assert.AreEqual(ValidTag1Visibility, tag.Visibility);
            Assert.AreEqual(ValidTag1MetaTitle, tag.MetaTitle);
            Assert.AreEqual(ValidTag1MetaDescription, tag.MetaDescription);
            Assert.AreEqual(null, tag.Count);
            Assert.AreEqual(ValidTag1Url, tag.Url);
        }

        [Test]
        public void GetTagBySlug_ReturnsPostCount_WhenIncludingCountPosts()
        {
            var tag = auth.GetTagBySlug(ValidTag1Slug, new TagQueryParams { IncludePostCount = true });

            Assert.AreEqual(ValidTag1Id, tag.Id);
            Assert.AreEqual(ValidTag1Slug, tag.Slug);
            Assert.AreEqual(ValidTag1Name, tag.Name);
            Assert.AreEqual(ValidTag1Description, tag.Description);
            Assert.AreEqual(ValidTag1FeatureImage, tag.FeatureImage);
            Assert.AreEqual(ValidTag1Visibility, tag.Visibility);
            Assert.AreEqual(ValidTag1MetaTitle, tag.MetaTitle);
            Assert.AreEqual(ValidTag1MetaDescription, tag.MetaDescription);
            Assert.AreEqual(ValidTag1PostCount, tag.Count.Posts);
            Assert.AreEqual(ValidTag1Url, tag.Url);
        }

        [Test]
        public void GetTagBySlug_ReturnsLimitedFields_WhenFieldsSpecified_ForIndividualRequest()
        {
            var tag = auth.GetTagBySlug(ValidTag1Slug, new TagQueryParams { Fields = TagFields.Id | TagFields.Slug | TagFields.Url });

            Assert.AreEqual(ValidTag1Id, tag.Id);
            Assert.AreEqual(ValidTag1Slug, tag.Slug);
            Assert.AreEqual(ValidTag1Url, tag.Url);

            Assert.IsNull(tag.Name);
            Assert.IsNull(tag.Description);
            Assert.IsNull(tag.FeatureImage);
            Assert.IsNull(tag.Visibility);
            Assert.IsNull(tag.MetaTitle);
            Assert.IsNull(tag.MetaDescription);
            Assert.IsNull(tag.Count);
        }

        [Test]
        public void GetTagBySlug_Throws_WhenTagAssociatedWithNoPublishedPosts()
        {
            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagBySlug(ValidTagWithNoPublishedPostsSlug));

            Assert.AreEqual("Tag not found.", ex.Message);
        }

        [Test]
        public void GetTags_ReturnsLimitedTags_WhenLimitSpecified()
        {
            var tagResponse = auth.GetTags(new TagQueryParams { Limit = 4, Fields = TagFields.Id });

            Assert.AreEqual(4, tagResponse.Tags.Count);
        }

        [Test]
        public void GetTags_ReturnsLimitedFields_WhenFieldsSpecified()
        {
            var tag = auth.GetTags(new TagQueryParams { Limit = 1, Fields = TagFields.Id }).Tags[0];

            Assert.IsNull(tag.Name);
            Assert.IsNull(tag.Description);
            Assert.IsNull(tag.FeatureImage);
            Assert.IsNull(tag.Visibility);
            Assert.IsNull(tag.MetaTitle);
            Assert.IsNull(tag.MetaDescription);
            Assert.IsNull(tag.Count);
            Assert.IsNull(tag.Slug);
            Assert.IsNull(tag.Url);

            Assert.IsNotNull(tag.Id);
        }

        [Test]
        public void GetTags_ReturnsPostCount_WhenIncludingCountPosts()
        {
            var tag = auth.GetTags(new TagQueryParams { Limit = 1, IncludePostCount = true }).Tags[0];

            Assert.IsNotNull(tag.Count.Posts);
        }

        [Test]
        public void GetTags_ReturnsAllTags_WhenNoLimitIsTrue()
        {
            var tagResponse = auth.GetTags(new TagQueryParams { Limit = 1, NoLimit = true, Fields = TagFields.Id });

            Assert.Greater(tagResponse.Tags.Count, MINIMUM_TAG_AMOUNT_THRESHHOLD);
        }

        [Test]
        public void GetTags_ReturnsExpectedTag_WhenOrderingByField()
        {
            var tag = auth.GetTags(new TagQueryParams { Limit = 1, Order = new List<Tuple<TagFields, OrderDirection>> { Tuple.Create(TagFields.Description, OrderDirection.desc) } }).Tags[0];

            Assert.AreEqual(ValidTag1Id, tag.Id);
            Assert.AreEqual(ValidTag1Slug, tag.Slug);
            Assert.AreEqual(ValidTag1Name, tag.Name);
            Assert.AreEqual(ValidTag1Description, tag.Description);
            Assert.AreEqual(ValidTag1FeatureImage, tag.FeatureImage);
            Assert.AreEqual(ValidTag1Visibility, tag.Visibility);
            Assert.AreEqual(ValidTag1MetaTitle, tag.MetaTitle);
            Assert.AreEqual(ValidTag1MetaDescription, tag.MetaDescription);
            Assert.AreEqual(null, tag.Count);
            Assert.AreEqual(ValidTag1Url, tag.Url);
        }

        [Test]
        public void GetTags_ReturnsExpectedTag_WhenGettingSecondPage()
        {
            var tag = auth.GetTags(new TagQueryParams { Limit = 1, Page = 2, Order = new List<Tuple<TagFields, OrderDirection>> { Tuple.Create(TagFields.Description, OrderDirection.desc) } }).Tags[0];

            Assert.AreEqual(ValidTag2Id, tag.Id);
            Assert.AreEqual(ValidTag2Slug, tag.Slug);
            Assert.AreEqual(ValidTag2Name, tag.Name);
            Assert.AreEqual(ValidTag2Description, tag.Description);

            Assert.IsNotNull(tag.FeatureImage);
            Assert.IsNotNull(tag.Visibility);
            Assert.IsNotNull(tag.MetaTitle);
            Assert.IsNotNull(tag.MetaDescription);
            Assert.IsNotNull(tag.Url);
        }

        [Test]
        public void GetTags_ReturnsExpectedTags_WhenApplyingFilter()
        {
            var tagResponse = auth.GetTags(new TagQueryParams { Filter = "slug:[gs-test,gs-test-2,gs-test-3]" });
            Assert.AreEqual(3, tagResponse.Tags.Count);

            var tag = tagResponse.Tags[0];
            Assert.AreEqual(ValidTag1Id, tag.Id);
            Assert.AreEqual(ValidTag1Slug, tag.Slug);
            Assert.AreEqual(ValidTag1Name, tag.Name);
            Assert.AreEqual(ValidTag1Description, tag.Description);
            Assert.AreEqual(ValidTag1FeatureImage, tag.FeatureImage);
            Assert.AreEqual(ValidTag1Visibility, tag.Visibility);
            Assert.AreEqual(ValidTag1MetaTitle, tag.MetaTitle);
            Assert.AreEqual(ValidTag1MetaDescription, tag.MetaDescription);
            Assert.AreEqual(null, tag.Count);
            Assert.AreEqual(ValidTag1Url, tag.Url);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTags_ThrowsException_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTags());
            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Unknown Content API Key", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetTags_ReturnsNull_WhenKeyIsInvalid_AndGhostExceptionsSuppressed(ExceptionLevel exceptionLevel)
        {
            auth = new GhostAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetTags());
            Assert.IsNotNull(auth.LastException);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTagById_ThrowsGhostSharpException_WhenIdIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagById(InvalidTagId));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Tag not found.", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetTagById_ReturnsNull_WhenKeyIsInvalid_AndGhostExceptionsSuppressed(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetTagById(InvalidTagId));
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetTagBySlug_ThrowsGhostSharpException_WhenSlugIsInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetTagBySlug(InvalidTagSlug));

            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Tag not found.", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetTagBySlug_ReturnsNull_WhenKeyIsInvalid_AndGhostExceptionsSuppressed(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsNull(auth.GetTagBySlug(InvalidTagSlug));
        }
    }
}
