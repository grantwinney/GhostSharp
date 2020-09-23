using GhostSharp.Entities;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class UploadThemeIntgTests : TestBase
    {
        private GhostAdminAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [Test]
        public void CreateThemeByFileAsBytes_Succeeds()
        {
            var themeFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AdminAPI", "sample_theme.zip");
            var randomThemeName = Guid.NewGuid().ToString();

            var theme = new ThemeRequest(File.ReadAllBytes(themeFilePath), $"{randomThemeName}.zip");

            var themeResponse = auth.UploadTheme(theme);

            Assert.AreEqual(randomThemeName, themeResponse.Name);
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
            Assert.IsFalse(themeResponse.Active);
            Assert.IsNull(themeResponse.Templates);
        }

        [Test]
        public void CreateThemeByFileName_Succeeds()
        {
            var themeFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AdminAPI", "sample_theme.zip");

            var theme = new ThemeRequest(themeFilePath);

            var themeResponse = auth.UploadTheme(theme);

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
            Assert.IsFalse(themeResponse.Active);
            Assert.IsNull(themeResponse.Templates);
        }
    }
}
