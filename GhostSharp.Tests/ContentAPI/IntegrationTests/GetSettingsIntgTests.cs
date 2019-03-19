using GhostSharp.Entities;
using NUnit.Framework;

namespace GhostSharp.Tests.ContentAPI.IntegrationTests
{
    [TestFixture]
    public class GetSettingsIntgTests : TestBase
    {
        [Test]
        public void GetSettings_ReturnsSettings_WhenKeyIsValid()
        {
            var auth = new GhostContentAPI(Host, ValidContentApiKey);

            Assert.AreEqual(SiteTitle, auth.GetSettings().Title);
        }

        [TestCase(ExceptionLevel.Ghost)]
        [TestCase(ExceptionLevel.All)]
        public void GetSettings_ThrowsException_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostContentAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            var ex = Assert.Throws<GhostSharpException>(() => auth.GetSettings());
            Assert.IsNotEmpty(ex.Errors);
            Assert.AreEqual("Unknown Content API Key", ex.Errors[0].Message);
        }

        [TestCase(ExceptionLevel.None)]
        [TestCase(ExceptionLevel.NonGhost)]
        public void GetSettings_DoesNotThrow_ReturnsNull_WhenKeyIsInvalid(ExceptionLevel exceptionLevel)
        {
            var auth = new GhostContentAPI(Host, InvalidApiKey) { ExceptionLevel = exceptionLevel };

            Assert.IsNull(auth.GetSettings());
            Assert.IsNotNull(auth.LastException);
        }
    }
}
