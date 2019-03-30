using GhostSharp.Entities;
using GhostSharp.Enums;
using NUnit.Framework;
using System;
using System.IO;

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

        //[Test]
        //public void CreatePost_CreatesBasicPost2()
        //{
        //    FileInfo fileInfo = new FileInfo(@"C:\Users\gwinney\Downloads\photo-1516828956617-80b9eec69bbb.jpg");

        //    // The byte[] to save the data in
        //    byte[] data = new byte[fileInfo.Length];

        //    // Load a filestream and put its content into the byte[]
        //    using (FileStream fs = fileInfo.OpenRead())
        //    {
        //        fs.Read(data, 0, data.Length);
        //    }

        //    // Delete the temporary file
        //    fileInfo.Delete();

        //    var expectedPost = new ImageRequest
        //    {
        //        Purpose = ImagePurpose.Image,
        //        Reference = "sample",
        //        File = data
        //    };

        //    var actualPost = auth.UploadImage(expectedPost);
        //}
    }
}
