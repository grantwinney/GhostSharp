using System;
using GhostSharp;
using GhostSharp.Entities;
using NUnit.Framework;

namespace GhostSharpTests
{
    [TestFixture]
    public class CreateTagTests : TestBase, IDisposable
    {
        readonly GhostAPI auth;
        Tag createdTag;

        public CreateTagTests()
        {
            createdTag = null;
            auth = new GhostAPI(Url, AuthToken);
        }

        [Test]
        public void CreateTag_ReturnsTag_WhenTagCreated()
        {
            string tagName = "tag_name_that_likely_does_not_exist";
            string tagSlug = "tag_slug_that_hopefully_does_not_exist";
            string tagDesc = "This is a wonderful description.";

            createdTag = auth.CreateTag(tagName, tagSlug, tagDesc);

            Assert.AreEqual(tagName, createdTag.Name);
            Assert.AreEqual(tagSlug, createdTag.Slug);
            Assert.AreEqual(tagDesc, createdTag.Description);
        }

        [Test]
        public void CreateTag_ReturnsTagWithSlugMatchingName_WhenSlugOmitted()
        {
            string tagName = "tag_name_that_likely_does_not_exist";
            string tagDesc = "This is a wonderful description.";

            createdTag = auth.CreateTag(tagName, null, tagDesc);

            Assert.AreEqual(tagName, createdTag.Name);
            Assert.AreEqual(tagName, createdTag.Slug);
            Assert.AreEqual(tagDesc, createdTag.Description);
        }

        [Test]
        public void CreateTag_ReturnsTagWithEmptyDescription_WhenDescriptionOmitted()
        {
            string tagName = "tag_name_that_likely_does_not_exist";
            string tagSlug = "tag_slug_that_hopefully_does_not_exist";

            createdTag = auth.CreateTag(tagName, tagSlug, null);

            Assert.AreEqual(tagName, createdTag.Name);
            Assert.AreEqual(tagSlug, createdTag.Slug);
            Assert.AreEqual("", createdTag.Description);
        }

        [Test]
        public void CreateTag_ThrowsException_WhenNameOmitted_AndSuppressionLevelNone()
        {
            auth.SuppressionLevel = SuppressionLevel.None;
            var ex = Assert.Throws<GhostSharpException>(() => createdTag = auth.CreateTag(null, "slug", "description"));

            Assert.IsNotEmpty(ex.Errors);
            StringAssert.StartsWith("ValidationError", ex.Errors[0].ErrorType);
            StringAssert.StartsWith("Value in [tags.name] cannot be blank", ex.Errors[0].Message);
        }

        [TestCase(SuppressionLevel.GhostOnly)]
        [TestCase(SuppressionLevel.All)]
        public void CreateTag_ReturnsNull_WhenNameOmitted_AndSuppressionLevelNotNone(SuppressionLevel level)
        {
            auth.SuppressionLevel = level;
            createdTag = auth.CreateTag(null, "slug", "description");

            Assert.Null(createdTag);
            Assert.NotNull(auth.LastException);
        }

        public void Dispose()
        {
            if (createdTag != null)
                auth.DeleteTagBySlug(createdTag.Slug);
            createdTag = null;
        }
    }
}
