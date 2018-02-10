using System;

namespace GhostSharpTests
{
    public partial class GhostSharpTests
    {
        // The base URL of your Ghost site, like https://your-blog.com
        const string Url = "";

        // The username and password used to access your site
        const string UserName = "";
        const string Password = "";

        // The client id and secret for your site
        const string ClientId = "";
        const string ClientSecret = "";

        // The authorization token used to access your site
        const string AuthToken = "";

        public GhostSharpTests()
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
