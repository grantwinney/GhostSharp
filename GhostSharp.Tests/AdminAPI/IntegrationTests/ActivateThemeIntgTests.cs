using NUnit.Framework;
using System.Threading;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class ActivateThemeIntgTests : TestBase
    {
        private GhostAdminAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [TearDown]
        public void TearDown()
        {
            // Activating a new theme seems to trigger a reboot of Ghost, which takes a few seconds to come back up.
            // The API call here fails if I don't wait a couple seconds at least.
            Thread.Sleep(5000);

            auth.ActivateTheme("symmetric");
        }

        [Test]
        public void ActivateTheme_Succeeds()
        {
            var themeResponse = auth.ActivateTheme("sample_theme");

            Assert.AreEqual("sample_theme", themeResponse.Name);
            Assert.AreEqual("casper", themeResponse.Package.Name);
            Assert.AreEqual("https://demo.ghost.io", themeResponse.Package.Demo);
            Assert.AreEqual("v3", themeResponse.Package.Engines.GhostAPI);
            Assert.AreEqual("MIT", themeResponse.Package.License);
            Assert.AreEqual("assets/screenshot-desktop.jpg", themeResponse.Package.ScreenShots.Desktop);
            Assert.AreEqual("gulp", themeResponse.Package.Scripts.Dev);
            Assert.AreEqual("Ghost Foundation", themeResponse.Package.Author.Name);
            Assert.Greater(themeResponse.Package.GPM.Categories.Count, 0);
            Assert.Greater(themeResponse.Package.Keywords.Count, 0);
            Assert.AreEqual("git", themeResponse.Package.Repository.Type);
            Assert.AreEqual("https://github.com/TryGhost/Casper/issues", themeResponse.Package.Bugs);
            Assert.Greater(themeResponse.Package.DevDependencies.Count, 10);
            Assert.Greater(themeResponse.Package.BrowsersList.Count, 0);
            Assert.AreEqual(25, themeResponse.Package.Config.PostsPerPage);
            Assert.AreEqual(30, themeResponse.Package.Config.ImageSizes.XXS.Width);
            Assert.AreEqual(1000, themeResponse.Package.Config.ImageSizes.L.Width);
            Assert.IsTrue(themeResponse.Active);
            Assert.GreaterOrEqual(0, themeResponse.Templates.Count);
        }
    }
}
