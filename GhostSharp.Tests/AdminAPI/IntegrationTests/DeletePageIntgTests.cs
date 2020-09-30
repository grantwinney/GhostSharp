using GhostSharp.Entities;
using GhostSharp.Enums;
using NUnit.Framework;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class DeletePageIntgTests : TestBase
    {
        private GhostAdminAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [Test]
        public void DeletePage_ReturnsTrue_WhenPageIdValid()
        {
            var newPageId = auth.CreatePage(new Post { Title = "This is a sample page for testing delete functionality" }).Id;

            Assert.IsTrue(auth.DeletePage(newPageId));
        }

        [TestCase(ExceptionLevel.NonGhost)]
        [TestCase(ExceptionLevel.None)]
        [Test]
        public void DeletePage_ReturnsFalse_WhenPageIdInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            Assert.IsFalse(auth.DeletePage(InvalidPageId));
        }

        [TestCase(ExceptionLevel.All)]
        [TestCase(ExceptionLevel.Ghost)]
        [Test]
        public void DeletePage_Throws_WhenPageIdInvalid(ExceptionLevel exceptionLevel)
        {
            auth.ExceptionLevel = exceptionLevel;

            var ex = Assert.Throws<GhostSharpException>(() => auth.DeletePage(InvalidPageId));

            Assert.That(ex.Message.StartsWith("Resource not found error, cannot delete page."));
        }
    }
}