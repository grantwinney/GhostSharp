using System;
using GhostSharp;
using Xunit;

namespace GhostSharpTests
{
    public class GET_AUTH_TOKEN : TestBase
    {
        //[Fact]
        public void GetYourVeryOwnAuthToken()
        {
            // IF YOU NEED AN AUTH TOKEN, UNCOMMENT THE ATTRIBUTE AND RUN THIS TEST.
            // Get your client id and secret (view the source code of any post),
            //   as well as your username and password, and paste them into TestBase.

            var auth = new GhostAPI(Url, ClientId, ClientSecret, UserName, Password);

            throw new Exception(auth.AuthorizationToken);
        }

        [Fact]
        public void IsPublicApiEnabled_ReturnsCorrectValue()
        {
            var auth = new GhostAPI(Url, ClientId, ClientSecret);

            // Try disabling the API on your site and change this to Assert.False
            Assert.True(auth.IsPublicApiEnabled());
        }
    }
}
