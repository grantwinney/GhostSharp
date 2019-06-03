using GhostSharp.Entities;
using GhostSharp.Enums;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

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
            //auth.ExceptionLevel = ExceptionLevel.None;
        }

        [Test]
        public void CreateImage_Succeeds()
        {
            var imageFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AdminAPI", "sample_image.jpeg");
            var randomImageName = Guid.NewGuid().ToString();

            var expectedPost = new ImageRequest(File.ReadAllBytes(imageFile), $"{randomImageName}.jpeg", ImageType.JPEG)
            {
                Purpose = ImagePurpose.Image,
                Reference = "sample"
            };

            var actualImage = auth.UploadImage(expectedPost);

            Assert.AreEqual($"{Host}content/images/{DateTime.Now.ToString("yyyy/MM")}/{randomImageName}.jpeg", actualImage.Url);
            Assert.IsNull(actualImage.Reference);  // why isn't this working?
        }

        // what happens when the purpose is invalid?
    }
}
