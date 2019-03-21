using GhostSharp.Enums;
using NUnit.Framework;
using System;
using System.Linq;

namespace GhostSharp.Tests.ContentAPI.UnitTests
{
    [TestFixture]
    public class ExtUnitTests
    {
        [TestCase(PostFields.Page | PostFields.Slug | PostFields.CommentId, "page,slug,comment_id")]
        [TestCase(PostFields.Uuid | PostFields.Excerpt | PostFields.TwitterImage, "uuid,twitter_image,excerpt")]
        [TestCase(PostFields.PublishedAt, "published_at")]
        [TestCase(0, "")]
        [TestCase(null, "")]
        public void GetQueryStringFromFlagsEnum_ReturnsCommaSeparatedListOfValues(PostFields input, string expectedResult)
        {
            var actualResult = Ext.GetQueryStringFromFlagsEnum<PostFields>(input);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetOrderQueryString_ReturnsCommaSeparatedListOfOrdering_WhenGivenMultipleFields()
        {
            var input = new[]
            {
                Tuple.Create(PostFields.CommentId, OrderDirection.desc),
                Tuple.Create(PostFields.Excerpt, OrderDirection.asc),
                Tuple.Create(PostFields.Id, OrderDirection.desc)
            }.ToList();

            var result = Ext.GetOrderQueryString(input);

            Assert.AreEqual("comment_id desc,excerpt asc,id desc", result);
        }

        [Test]
        public void GetOrderQueryString_ReturnsValue_WhenGivenSingleField()
        {
            var input = new[]
            {
                Tuple.Create(PostFields.Slug, OrderDirection.desc)
            }.ToList();

            var result = Ext.GetOrderQueryString(input);

            Assert.AreEqual("slug desc", result);
        }

        [Test]
        public void GetOrderQueryString_OmitsInvalidField()
        {
            var input = new[]
            {
                Tuple.Create(PostFields.CommentId, OrderDirection.desc),
                Tuple.Create((PostFields)0, OrderDirection.asc),
                Tuple.Create(PostFields.Id, OrderDirection.desc),
                Tuple.Create((PostFields)333, OrderDirection.desc),
            }.ToList();

            var result = Ext.GetOrderQueryString(input);

            Assert.AreEqual("comment_id desc,id desc", result);
        }

        [TestCase(0, 0)]
        [TestCase(99999, 0)]
        [TestCase(null, 0)]
        public void GetOrderQueryString_ReturnsEmptyString_WhenInvalidField(PostFields postFields, OrderDirection orderDirection)
        {
            var input = new[]
            {
                Tuple.Create(postFields, orderDirection)
            }.ToList();

            var result = Ext.GetOrderQueryString(input);

            Assert.AreEqual("", result);
        }

        [TestCase(PostFields.Excerpt, "excerpt")]
        [TestCase(PostFields.CreatedAt, "created_at")]
        [TestCase(0, null)]
        [TestCase(333, null)]
        public void GetFieldName_ReturnsCorrectFieldName(PostFields input, string expectedResult)
        {
            Assert.AreEqual(expectedResult, Ext.GetFieldName(input));
        }

        [TestCase("abcdef", new byte[] { 171, 205, 239})]
        [TestCase("123456", new byte[] { 18, 52, 86})]
        [TestCase("deadbeef", new byte[] { 222, 173, 190, 239})]
        public void StringToByteArray_ReturnsCorrectValues(string hex, byte[] expectedByteArray)
        {
            Assert.AreEqual(expectedByteArray, Ext.StringToByteArray(hex));
        }
    }
}
