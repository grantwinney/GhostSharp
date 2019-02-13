using System;
using GhostSharp;
using GhostSharp.Entities;
using NUnit.Framework;

namespace GhostSharpTests
{
    [TestFixture]
    public class GET_AUTH_TOKEN : TestBase
    {
        //[Test]
        public void GetYourVeryOwnAuthToken()
        {
            // IF YOU NEED AN AUTH TOKEN, UNCOMMENT THE ATTRIBUTE AND RUN THIS TEST.
            // Get your client id and secret (view the source code of any post),
            //   as well as your username and password, and paste them into TestBase.

            var auth = new GhostAPI(Url, ClientId, ClientSecret, UserName, Password);

            throw new Exception(auth.AuthorizationToken);
        }

        [Test]
        public void IsPublicApiEnabled_ReturnsCorrectValue()
        {
            var auth = new GhostAPI(Url, ClientId, ClientSecret)
            {
                SuppressionLevel = SuppressionLevel.GhostOnly
            };

            // Try disabling the API on your site and change this to Assert.False
            Assert.True(auth.IsPublicApiEnabled());
        }
    }
}
