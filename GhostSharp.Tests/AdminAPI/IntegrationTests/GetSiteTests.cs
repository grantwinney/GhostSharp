using NUnit.Framework;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class GetSiteIntgTests : TestBase
    {
        [Test]
        public void GetSite_ReturnsSiteInfo_WhenAuthorized()
        {
            var auth = new GhostAdminAPI(Host, ValidAdminApiKey);

            var site = auth.GetSite();

            Assert.AreEqual(SiteTitle, site.Title);
            Assert.AreEqual(SiteDescription, site.Description);
            Assert.IsNull(site.Logo);
            Assert.AreEqual(Host, site.Url);
            Assert.IsNotNull(site.Version);
        }

        [Test]
        public void GetSite_ReturnsSiteInfo_WhenNotAuthorized()
        {
            var auth = new GhostAdminAPI(Host);

            var site = auth.GetSite();

            Assert.AreEqual(SiteTitle, site.Title);
            Assert.AreEqual(SiteDescription, site.Description);
            Assert.IsNull(site.Logo);
            Assert.AreEqual(Host, site.Url);
            Assert.IsNotNull(site.Version);
        }
    }
}
