using GhostSharp.Entities;
using GhostSharp.Enums;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace GhostSharp.Tests.AdminAPI.IntegrationTests
{
    [TestFixture]
    public class UploadImageIntgTests : TestBase
    {
        private GhostAdminAPI auth;

        [SetUp]
        public void SetUp()
        {
            auth = new GhostAdminAPI(Host, ValidAdminApiKey);
        }

        [Test]
        public void CreateImageByFileAsBytes_Succeeds()
        {
            var imageFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AdminAPI", "sample_image.jpeg");
            var randomImageName = Guid.NewGuid().ToString();

            var expectedPost = new ImageRequest(File.ReadAllBytes(imageFilePath), $"{randomImageName}.jpeg", ImageType.JPEG)
            {
                Purpose = ImagePurpose.Image,
                Reference = "sample"
            };

            var actualImage = auth.UploadImage(expectedPost);

            Assert.AreEqual($"{Host}content/images/{DateTime.Now:yyyy/MM}/{randomImageName}.jpeg", actualImage.Url);
            Assert.AreEqual(expectedPost.Reference, actualImage.Reference);  // why isn't this working?
        }

        [Test]
        public void CreateImageByFileName_Succeeds()
        {
            var imageFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AdminAPI", "sample_image.jpeg");

            var expectedPost = new ImageRequest(imageFilePath)
            {
                Purpose = ImagePurpose.Image,
                Reference = "sample"
            };

            var actualImage = auth.UploadImage(expectedPost);

            var imageFilePattern = Regex.Escape($"{Host}content/images/{DateTime.Now:yyyy/MM}/{Path.GetFileNameWithoutExtension(imageFilePath)}") + @"-?\d*" + Regex.Escape(".jpeg");
            Assert.IsTrue(Regex.IsMatch(actualImage.Url, imageFilePattern));
            Assert.AreEqual(expectedPost.Reference, actualImage.Reference);  // why isn't this working?
        }
    }
}
