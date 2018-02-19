using System;

namespace GhostSharpTests
{
    public class TestBase
    {
        // The base URL of your Ghost site, like https://your-blog.com
        public const string Url = "";

        // The username and password used to access your site
        public const string UserName = "";
        public const string Password = "";

        // The client id and secret for your site
        public const string ClientId = "";
        public const string ClientSecret = "";

        // The authorization token used to access your site
        // If you don't have one, uncomment and run the GET_AUTH_TOKEN.GetYourVeryOwnAuthToken() test
        public const string AuthToken = "";

        // The ID and Slug of the user who generated the auth token (for GET tests in GetUserTests)
        public const string UserId = "";
        public const string UserSlug = "";

        public TestBase()
        {
            if (String.IsNullOrWhiteSpace(Url) ||
                String.IsNullOrWhiteSpace(UserName) || String.IsNullOrWhiteSpace(Password) ||
                String.IsNullOrWhiteSpace(ClientId) || String.IsNullOrWhiteSpace(ClientSecret) ||
                String.IsNullOrWhiteSpace(AuthToken))
            {
                throw new ApplicationException("Fill in all configuration values before running tests.");
            }
        }
    }
}
